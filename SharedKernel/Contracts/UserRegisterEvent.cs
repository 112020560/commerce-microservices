using MassTransit;

namespace SharedKernel.Contracts;

[EntityName("user-create-event")]
public record  UserRegisterEvent(Guid AggregateId, string AggregateType, string Name, int Stock) : IEvent
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;
}
