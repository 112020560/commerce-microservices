using Infrastructure.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddServices();
            //.AddAuthorizationInternal();
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
    // private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    // {
    //     var connectionString = configuration.GetConnectionString("PostgresConnection") ??
    //                           throw new Exception("No fue posible cargar el string de conexion");
    //
    //     services.AddDbContext<CommerceDbContext>(options =>
    //     {
    //         options.UseNpgsql(connectionString)
    //         .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Debug)
    //             .EnableSensitiveDataLogging()
    //             .EnableDetailedErrors()
    //             .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //     });
    //
    //     services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<CommerceDbContext>());
    //
    //     services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    //     services.AddScoped<IAuthUnitOfWork, AuthUnitOfWork>();
    //     //Repositories
    //     //Auth
    //     services.AddScoped<IUserRepository, UserRepository>();
    //     //Crm
    //     services.AddScoped<IAddressRepository, AddressRepository>();
    //     services.AddScoped<IAttributeRepository, AttributeRepository>();
    //     services.AddScoped<IPersonRelationshipRepository, PersonRelationshipRepository>();
    //     services.AddScoped<IPersonRepository, PersonRepository>();
    //     //Retail
    //     services.AddScoped<IProductRepository, ProductRepository>();
    //     services.AddScoped<IDocumentsRepository, DocumentsRepository>();
    //     services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();
    //     services.AddScoped<IInventoryRepository, InventoryRepository>();
    //
    //     return services;
    // }

    // private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    // {
    //     services
    //         .AddHealthChecks()
    //         .AddNpgSql(configuration.GetConnectionString("PostgresConnection")!);
    //
    //     return services;
    // }
    //
    // private static IServiceCollection AddAuthenticationInternal(this IServiceCollection services, IConfiguration configuration)
    // {
    //     services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //         .AddJwtBearer(o =>
    //         {
    //             o.RequireHttpsMetadata = false;
    //             o.TokenValidationParameters = new TokenValidationParameters
    //             {
    //                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
    //                 ValidIssuer = configuration["Jwt:Issuer"],
    //                 ValidAudience = configuration["Jwt:Audience"],
    //                 ClockSkew = TimeSpan.Zero
    //             };
    //         });
    //         
    //     services.AddSingleton<IPasswordHasher, PasswordHasher>();
    //     services.AddSingleton<ITokenProvider, TokenProvider>();
    //     return services;
    // }
}
