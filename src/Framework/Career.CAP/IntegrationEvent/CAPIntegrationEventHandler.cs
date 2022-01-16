using System.Threading.Tasks;
using Career.EventHub;
using DotNetCore.CAP;

namespace Career.CAP.IntegrationEvent;

public abstract class CAPIntegrationEventHandler<TEvent> : IIntegrationEventHandler<TEvent>, ICapSubscribe where TEvent : IIntegrationEvent
{
    public abstract Task Handle(TEvent @event);
}