
namespace WebApp.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using WebApp.Application.Abstractions;
using WebApp.Infrastructure.Services;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}