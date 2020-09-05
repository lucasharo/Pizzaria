using Service.Interfaces;
using Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.Interfaces;
using Services.Services;

namespace API.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection AddDependencyInjectionSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IPedidoService, PedidoService>();

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITokenService, TokenService>();

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}