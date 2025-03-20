using System;
using EventSourcing.Application.Create;
using MassTransit;
using MediatR;
using SharedKernel.Contracts;

namespace EventSourcing.Worker.Consumer;

public class ProductEventSourcingConsumer(IMediator mediator) : IConsumer<ProductCreatedEvent>
{
    public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        await mediator.Publish(
            new CreateEventSoursingCommand(
                context.Message.AggregateId,
                context.Message.EventType,
                context.Message.EventData,
                context.Message.Timestamp));
    }
}
