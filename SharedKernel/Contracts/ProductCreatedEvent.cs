using MassTransit;

namespace SharedKernel.Contracts;

[EntityName("product-create-event")]
public record ProductCreatedEvent(Guid AggregateId, string AggregateType, string Name, int Stock) : IEvent
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;
}
