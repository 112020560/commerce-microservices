using SharedKernel.Contracts;

namespace SharedKernel;

public interface IProducerService
{
    Task SendCommand<TBody>(TBody payload, string command,string traceId, CancellationToken cancellationToken);
     Task PublishEvent<TBody>(TBody payload, string traceId, CancellationToken cancellationToken);
     Task<object> RequestResponse(IEvent transferData, CancellationToken cancellationToken);
}
