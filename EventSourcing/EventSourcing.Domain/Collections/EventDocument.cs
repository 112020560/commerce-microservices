using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace EventSourcing.Domain.Collections;

public class EventDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
        
    // Identificador del agregado original (por ejemplo, ID de usuario en MySQL)
    public Guid AggregateId { get; set; }
        
    // Tipo de evento para deserialización polimórfica
    public string? EventType { get; set; }
        
    // Datos del evento serializados como JSON
    public string? EventData { get; set; }
        
    // Versión para control de concurrencia
    public long Version { get; set; }
        
    // Timestamp del evento
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}