using System;
using MassTransit;
using Microsoft.Extensions.Configuration;
using SharedKernel;
using SharedKernel.Contracts;

namespace Infrastructure.RabbitMq;

public class ProducerService: IProducerService
{
    private readonly IBus _bus;
    private readonly IRequestClient<IEvent> _client;
    private readonly IConfiguration _configuration;
    
    public ProducerService(IBus bus, IRequestClient<IEvent> client, IConfiguration configuration)
    {
        _bus = bus;
        _client = client;
        _configuration = configuration;
    }
    
    public async Task SendCommand<TBody>(TBody payload, string command,string traceId, CancellationToken cancellationToken)
    {
        var rabbitHost = _configuration.GetSection("RabbitMqSettings:Uri").Value ?? Environment.GetEnvironmentVariable("RABBIT_HOST") ??
            throw new Exception("The rabbitMQ connection was not supled");

        if (payload is null) throw new Exception("The SendCommand payload is null");
        var url = new Uri($"{rabbitHost}/{command}");

        var endpoint = await _bus.GetSendEndpoint(url);
        await endpoint.Send(payload,sendContext =>
        {
            sendContext.Headers.Set("X-Trace-Id", traceId);
        }, cancellationToken);
        //return Ok("Command sent successfully");
    }
    
    public async Task PublishEvent<TBody>(TBody payload, string traceId, CancellationToken cancellationToken)
    {
        if (payload is null) throw new Exception("The PublishEvent payload is null");
        await _bus.Publish(payload, sendContext =>
        {
            sendContext.Headers.Set("X-Trace-Id", traceId);
        },cancellationToken);
    }
    
    public async Task<object> RequestResponse(IEvent transferData, CancellationToken cancellationToken)
    {
        var request = _client.Create(transferData, cancellationToken );
        var response = await request.GetResponse<object>();

        return response.Message;
    }
}
