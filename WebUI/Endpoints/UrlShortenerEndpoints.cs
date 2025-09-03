using Application.Features.URLTokens.Requests.Command;
using Application.Features.URLTokens.Requests.Query;
using MediatR;
using Persistence;

namespace WebUI.Endpoints
{
    public static class UrlShortenerEndpoints
    {
        public static WebApplication MapUrlShortenerEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("");

            group.MapGet("/r/{token}", async (string token, IMediator mediator, HttpContext ctx) =>
            {
                Console.WriteLine($"Token is: {token}");

                var resp = await mediator.Send(new GetURLTokenDetailByTokenQuery { Token = token });

                Console.WriteLine($"Response: {resp}");
                Console.WriteLine($"Response Data: {resp.Data}");
                Console.WriteLine($"Response Success: {resp.Success}");

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

                return Results.Redirect(resp?.Data?.OriginalUrl);
            });

            return app;
        }
    }
}
