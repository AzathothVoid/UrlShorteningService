using MediatR;
using Persistence;

namespace WebUI.Endpoints
{
    public static class UrlShortenerEndpoints
    {
        public static WebApplication MapUrlShortenerEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("");

            group.MapGet("/r/{token}", async (string token, UrlShortenerDbContext db, HttpContext ctx) =>
            {
                

                return Results.Redirect(entity.OriginalUrl);
            });

            return app;
        }
    }
}
