﻿using EventSourcing.Application.Abstractions.Behaviors;
using FluentValidation;
using Inventories.Application.Abstractions.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddEventSourcingApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        return services;
    }
}