using Application.Contracts.Infrastructure;
using Application.Features.URLTokens.Requests.Command;
using Application.Features.URLTokens.Requests.Query;
using Application.Models.Visit;
using MediatR;
using UAParser;

namespace WebUI.Endpoints
{
    public static class UrlShortenerEndpoints
    {
        public static WebApplication MapUrlShortenerEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("");

            group.MapGet("/r/{token}", async (string token, IMediator mediator, HttpContext ctx, IVisitQueue queue) =>
            {
                var resp = await mediator.Send(new GetURLTokenDetailByTokenQuery { Token = token });
       
                if (resp == null || resp.Success == false || resp.Data == null)
                {               
                    var encoded = Uri.EscapeDataString(token ?? "");
                    return Results.Redirect($"/invalid?token={encoded}");
                }

                var clientInfo = Parser.GetDefault().Parse(ctx.Request.Headers["User-Agent"].FirstOrDefault() ?? "");

                if (resp.Data.ExpiresAt.HasValue && resp.Data.ExpiresAt.Value < DateTime.UtcNow)
                {
                    var encoded = Uri.EscapeDataString(token ?? "");
                    return Results.Redirect($"/invalid?token={encoded}&reason=expired");
                }

                await mediator.Send(new UpdateClickURLTokenCommand { Token = token });

                var deviceType = clientInfo.Device.Family;
                if (deviceType == "Other")
                {
                    if (clientInfo.OS.Family.Contains("Android") || clientInfo.OS.Family.Contains("iOS"))
                        deviceType = "Mobile";
                    else if (clientInfo.OS.Family.Contains("Windows") || clientInfo.OS.Family.Contains("Mac") || clientInfo.OS.Family.Contains("Linux"))
                        deviceType = "Desktop";
                }

                var evt = new VisitEvent
                {
                    URLTokenId = resp.Data.Id,
                    Timestamp = DateTimeOffset.UtcNow,
                    IpAddress = ctx.Connection.RemoteIpAddress?.ToString(),
                    UserAgent = ctx.Request.Headers["User-Agent"].FirstOrDefault(),
                    Referrer = ctx.Request.Headers["Referer"].FirstOrDefault(),
                    DeviceType = deviceType,
                    Browser = clientInfo.UA.Family,
                    IsBot = clientInfo.UA.Family.ToLower().Contains("bot") || clientInfo.UA.Family.ToLower().Contains("spider") || clientInfo.UA.Family.ToLower().Contains("crawl") || clientInfo.Device.IsSpider
                };

                await queue.EnqueueAsync(evt); // non-blocking

                return Results.Redirect(resp?.Data?.OriginalUrl);
            });

            return app;
        }
    }
}
