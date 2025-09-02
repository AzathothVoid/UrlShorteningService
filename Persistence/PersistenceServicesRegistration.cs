using Application.Contracts.Persistence;
using Application.Contracts.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using Persistence.Repositories.Common;

namespace Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UrlShortenerDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("URLShortenerConnectionString"))
            );

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IURLTokenRepository, URLTokenRepository>();

            return services;
        }

    }
}
