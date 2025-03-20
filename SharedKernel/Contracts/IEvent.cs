namespace SharedKernel.Contracts;

public interface IEvent
{
    Guid AggregateId { get; }
    string AggregateType { get; }
    DateTime Timestamp { get; }
}



