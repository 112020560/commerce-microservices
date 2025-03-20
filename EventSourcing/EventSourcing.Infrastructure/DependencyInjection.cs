using EventSourcing.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace EventSourcing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddEventSourcing(
        this IServiceCollection services,
        string mongoConnectionString,
        string databaseName)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        // Configurar cliente de MongoDB
        services.AddSingleton<IMongoClient>(sp =>
            new MongoClient(mongoConnectionString));

        // Configurar base de datos
        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });

        // Registrar servicio de Event Sourcing
        services.AddScoped<IEventSourcingService, MongoEventSourcingService>();

        return services;
    }
}