using MassTransit;

namespace SharedKernel.Contracts;

[EntityName("person-create-event")]
public record  PersonCreatedEvent(Guid AggregateId, string AggregateType, string Name, int Stock) : IEvent
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;
}
