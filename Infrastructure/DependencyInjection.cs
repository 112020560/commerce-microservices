using Application.Abstractions.Data;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
         var connectionString = configuration.GetConnectionString("PostgresConnection") ??
                               throw new Exception("No fue posible cargar el string de conexion");

        services.AddDbContext<CommerceDbContext>(options => 
        {
            options.UseNpgsql(connectionString)
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Debug)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
