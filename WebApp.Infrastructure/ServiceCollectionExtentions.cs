using Microsoft.Extensions.DependencyInjection;
using WebApp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using WebApp.Application.Abstractions;

namespace WebApp.Infrastructure;



public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();

        var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
        Console.WriteLine($"Email: {emailSettings.EmailId}, Host: {emailSettings.Host}");

        return services;
    }
}