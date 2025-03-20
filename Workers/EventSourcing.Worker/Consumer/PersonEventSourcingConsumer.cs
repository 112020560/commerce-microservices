using System;
using EventSourcing.Application.Create;
using MassTransit;
using MediatR;
using SharedKernel.Contracts;

namespace EventSourcing.Worker.Consumer;

public class PersonEventSourcingConsumer(IMediator mediator) : IConsumer<PersonCreatedEvent>
{
    public async Task Consume(ConsumeContext<PersonCreatedEvent> context)
    {
        await mediator.Publish(
            new CreateEventSoursingCommand(
                context.Message.AggregateId,
                context.Message.EventType,
                context.Message.EventData,
                context.Message.Timestamp));
    }
}
