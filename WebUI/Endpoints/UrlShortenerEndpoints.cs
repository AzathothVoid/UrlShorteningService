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
                var resp = await mediator.Send(new GetURLTokenDetailByTokenQuery { Token = token });

                if (resp == null || resp.Success == false)
                    throw new Exception();

                return Results.Redirect(resp?.Data?.OriginalUrl);
            });

            return app;
        }
    }
}
