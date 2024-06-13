using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Application.Common.Mappings;

namespace WebApp.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(assembly));

        services.AddValidatorsFromAssembly(assembly);

        services.AddAutoMapper(typeof(MappingProfiles));

        return services;
    }
}
