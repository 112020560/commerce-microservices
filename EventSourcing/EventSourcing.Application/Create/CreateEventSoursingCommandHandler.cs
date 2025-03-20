using System;
using EventSourcing.Application.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventSourcing.Application.Person.Create;

internal sealed class CreateEventSoursingCommandHandler(IEventSourcingService eventSourcingService, ILogger<CreateEventSoursingCommandHandler> logger) : INotificationHandler<CreateEventSoursingCommand>
{
    public async Task Handle(CreateEventSoursingCommand notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Eventsourcong register");
        await eventSourcingService.RecordEventAsync(notification.AggregateId, notification.EventType, notification.EventData);
    }
}
