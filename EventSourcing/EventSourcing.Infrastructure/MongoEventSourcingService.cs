using EventSourcing.Application.Abstractions;
using EventSourcing.Domain.Collections;
using MongoDB.Driver;

namespace EventSourcing.Infrastructure;

public class MongoEventSourcingService : IEventSourcingService
{
    private readonly IMongoCollection<EventDocument> _eventCollection;
    private readonly IMongoDatabase _database;

    public MongoEventSourcingService(IMongoDatabase database)
    {
        _database = database;
        _eventCollection = database.GetCollection<EventDocument>("DomainEvents");

        // Crear índices para rendimiento
        var indexKeys = Builders<EventDocument>.IndexKeys
            .Ascending(x => x.AggregateId)
            .Ascending(x => x.Version);
            
        _eventCollection.Indexes.CreateOne(
            new CreateIndexModel<EventDocument>(indexKeys, 
                new CreateIndexOptions { Unique = true })
        );
    }

    public async Task RecordEventAsync(Guid aggregateId, string eventType, object eventData)
    {
        // Obtener la última versión para este agregado
        var lastVersion = await _eventCollection
            .Find(e => e.AggregateId == aggregateId)
            .SortByDescending(e => e.Version)
            .FirstOrDefaultAsync();

        var newEvent = new EventDocument
        {
            AggregateId = aggregateId,
            EventType = eventType,
            EventData = System.Text.Json.JsonSerializer.Serialize(eventData),
            Version = lastVersion?.Version + 1 ?? 1,
            Timestamp = DateTime.UtcNow
        };

        await _eventCollection.InsertOneAsync(newEvent);
    }

    public async Task<List<EventDocument>> GetEventsForAggregateAsync(Guid aggregateId)
    {
        return await _eventCollection
            .Find(e => e.AggregateId == aggregateId)
            .SortBy(e => e.Version)
            .ToListAsync();
    }
}