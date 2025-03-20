using MassTransit;

namespace SharedKernel.Contracts;

[EntityName("product-create-event")]
public record ProductCreatedEvent(Guid AggregateId, string EventType, object EventData) : IEvent
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;
}
