using MassTransit;

namespace SharedKernel.Contracts;

[EntityName("user-create-event")]
public record  UserRegisterEvent(Guid AggregateId, string EventType, object EventData) : IEvent
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;
}
