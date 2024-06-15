using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Application.Behaviors;
using WebApp.Application.Common.Mappings;

namespace WebApp.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        services.AddAutoMapper(typeof(MappingProfiles));

        return services;
    }
}
