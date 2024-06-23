using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Domain.Repositories;
using WebApp.Persistence.Repositories;

namespace WebApp.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddCaching(configuration);
        return services;
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultPostgreConnection");

        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(connectionString, builder => builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
        });
    }

    private static void AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.Decorate<IMemberRepository, CachedMemberRepository>();

        services.AddStackExchangeRedisCache(redisOptions =>
        {
            string? connection = configuration.GetConnectionString("Redis");

            redisOptions.Configuration = connection;
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
            //.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IMemberRepository, MemberRepository>()
            .AddScoped<IGatheringRepository, GatheringRepository>()
            .AddScoped<IInvitationRepository, InvitationRepository>()
            .AddScoped<IAttendeeRepository, AttendeeRepository>();

    }

}

