using System;
using EventSourcing.Application.Create;
using MassTransit;
using MediatR;
using SharedKernel.Contracts;

namespace EventSourcing.Worker.Consumer;

public class UsersEventSourcingConsumer(IMediator mediator, ILogger<UsersEventSourcingConsumer> logger) : IConsumer<UserRegisterEvent>
{
    public async Task Consume(ConsumeContext<UserRegisterEvent> context)
    {
        try
        {
            await mediator.Publish(
            new CreateEventSoursingCommand(
                context.Message.AggregateId,
                context.Message.EventType,
                context.Message.EventData,
                context.Message.Timestamp));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "UsersEventSourcingConsumer Error: {error}", ex.Message);
        }
        
    }
}
