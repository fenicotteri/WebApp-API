using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WebApp.Infrastructure.Services.Email;
using WebApp.Application.Abstractions.Services;
using WebApp.Infrastructure.Services.Caching;

namespace WebApp.Infrastructure;



public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEmailService, EmailService>();
        services.AddCaching(configuration);

        return services;
    }

    private static void AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICacheService, CacheService>();

        services.AddStackExchangeRedisCache(redisOptions =>
        {
            string? connection = configuration.GetConnectionString("Redis");

            redisOptions.Configuration = connection;
        });
    }
}