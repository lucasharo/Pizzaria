using Service.Interfaces;
using Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection AddDependencyInjectionSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IPedidoService, PedidoService>();

            return services;
        }
    }
}