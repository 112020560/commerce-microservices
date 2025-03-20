using EventSourcing.Domain.Collections;

namespace EventSourcing.Application.Abstractions;

public interface IEventSourcingService
{
    // Método para registrar un evento de dominio
    Task RecordEventAsync(Guid aggregateId, string eventType, object eventData);
        
    // Método para recuperar eventos de un agregado específico
    Task<List<EventDocument>> GetEventsForAggregateAsync(Guid aggregateId);
}