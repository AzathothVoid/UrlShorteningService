using Application.Contracts.Infrastructure;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IURLTokenService, URLTokenService>();
            services.AddSingleton<IVisitQueue, VisitQueue>();
            services.AddHostedService<VisitWriterService>();

            return services;
        }
    }
}
