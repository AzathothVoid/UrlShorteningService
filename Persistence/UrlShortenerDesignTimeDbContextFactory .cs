using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Persistence
{
    public class UrlShortenerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<UrlShortenerDbContext>
    {
        public UrlShortenerDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory(); 
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

        
            var conn = configuration.GetConnectionString("URLShortenerConnectionString")
                       ?? Environment.GetEnvironmentVariable("URLSHORTENER_CONN")
                       ?? "Host=localhost;Database=urlshortener;Username=postgres;Password=postgres";

            var optionsBuilder = new DbContextOptionsBuilder<UrlShortenerDbContext>();

          
            optionsBuilder.UseNpgsql(conn);

            return new UrlShortenerDbContext(optionsBuilder.Options);
        }
    }
}
