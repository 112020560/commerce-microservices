namespace SharedKernel.Contracts;

public interface IEvent
{
    Guid AggregateId { get;}
    string EventType { get;}
    object EventData { get;}
}



