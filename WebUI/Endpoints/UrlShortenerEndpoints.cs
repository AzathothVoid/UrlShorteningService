using Application.Features.URLTokens.Requests.Command;
using Application.Features.URLTokens.Requests.Query;
using Application.Models.Visit;
using MediatR;
using Persistence;

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

                if (resp.Data.ExpiresAt.HasValue && resp.Data.ExpiresAt.Value < DateTime.UtcNow)
                {
                    var encoded = Uri.EscapeDataString(token ?? "");
                    return Results.Redirect($"/invalid?token={encoded}&reason=expired");
                }

                await mediator.Send(new UpdateClickURLTokenCommand { Token = token });

                var evt = new VisitEvent
                {
                    URLTokenId = resp.Data.Id,
                    Timestamp = DateTimeOffset.UtcNow,
                    IpAddress = ctx.Connection.RemoteIpAddress?.ToString(),
                    UserAgent = ctx.Request.Headers["User-Agent"].FirstOrDefault(),
                    Referrer = ctx.Request.Headers["Referer"].FirstOrDefault()
                };
                await queue.EnqueueAsync(evt); // non-blocking

                return Results.Redirect(resp?.Data?.OriginalUrl);
            });

            return app;
        }
    }
}
