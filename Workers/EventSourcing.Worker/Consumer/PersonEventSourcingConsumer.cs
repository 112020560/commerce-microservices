using System;
using MassTransit;
using SharedKernel.Contracts;

namespace EventSourcing.Worker.Consumer;

public class EventSourcingConsumer : IConsumer<IEvent>
{
    public Task Consume(ConsumeContext<IEvent> context)
    {
        throw new NotImplementedException();
    }
}
