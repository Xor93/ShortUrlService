using Microsoft.Extensions.DependencyInjection;
using System;
using TinyURL.Options;
using TinyURL.Services;
using TinyURL.Services.Interfaces;

namespace TinyURL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUrlService(this IServiceCollection services)
        {
            services.AddScoped<IUrlService, UrlService>();
            return services;
        }

        public static IServiceCollection AddHashService(this IServiceCollection services, Action<HashOptions> options)
        {
            services.AddScoped<IHashService, HashService>();
            services.Configure(options);
            return services;
        }

        public static IServiceCollection AddDateService(this IServiceCollection services)
        {
            services.AddScoped<IDateService, DateService>();
            return services;
        }

        public static IServiceCollection AddNumberGenerateService(this IServiceCollection services, Action<RandomNumberOptions> options)
        {
            services.AddScoped<INumberGenerateService, NumberGenerateService>();
            services.Configure(options);
            return services;
        }
    }
}
