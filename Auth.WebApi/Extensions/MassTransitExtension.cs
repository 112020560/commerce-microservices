using System;
using Infrastructure.RabbitMq;
using Infrastructure.Time;
using MassTransit;
using SharedKernel;
using SharedKernel.Contracts;

namespace Auth.WebApi.Extensions;

public static class MassTransitExtension
{
    public static void ConfigureMasstransit(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitHost = configuration.GetSection("RabbitMqSettings:Uri").Value ?? Environment.GetEnvironmentVariable("RABBIT_HOST") ??
                throw new Exception("The rabbitMQ connection was not supled");
        
        services.AddMassTransit(x =>
        {
            x.AddRequestClient<UserRegisterEvent>();
            x.UsingRabbitMq((context, config) =>
            {
                config.Host(rabbitHost);
            });
        });

        
    }

    public static IServiceCollection ConfigureSharedService(this IServiceCollection services)
    {
        services.AddScoped<IProducerService, ProducerService>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}
