using MediatR;

namespace EventSourcing.Application.Create;

public record CreateEventSoursingCommand(Guid AggregateId,  string EventType, object EventData, DateTime Timestamp): INotification;
