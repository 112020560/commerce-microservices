using System.Text;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Data.Auth;
using Application.Abstractions.Data.Crm;
using Application.Abstractions.Data.Retail;
using Infrastructure.Authentication;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Auth;
using Infrastructure.Persistence.Repositories.Crm;
using Infrastructure.Persistence.Repositories.Retail;
using Infrastructure.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddServices()
            .AddDatabase(configuration)
            .AddHealthChecks(configuration)
            .AddAuthenticationInternal(configuration);
            //.AddAuthorizationInternal();
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
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

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<CommerceDbContext>());

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAuthUnitOfWork, AuthUnitOfWork>();
        //Repositories
        //Auth
        services.AddScoped<IUserRepository, UserRepository>();
        //Crm
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IAttributeRepository, AttributeRepository>();
        services.AddScoped<IPersonRelationshipRepository, PersonRelationshipRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        //Retail
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IDocumentsRepository, DocumentsRepository>();
        services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("PostgresConnection")!);

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });
            
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();
        return services;
    }
}
