using System.Threading.Tasks;

namespace Career.EventHub;

public interface IIntegrationEventHandler<TEvent>  where TEvent : IIntegrationEvent
{
    Task Handle(TEvent @event);
}