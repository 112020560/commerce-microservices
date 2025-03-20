using MassTransit;

namespace SharedKernel.Contracts;

[EntityName("person-create-event")]
public record  PersonCreatedEvent(Guid AggregateId, string EventType, object EventData) : IEvent
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;

}
