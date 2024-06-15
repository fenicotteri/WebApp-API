using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WebApp.Infrastructure.Services.Email;
using WebApp.Application.Abstractions.Services;

namespace WebApp.Infrastructure;



public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}