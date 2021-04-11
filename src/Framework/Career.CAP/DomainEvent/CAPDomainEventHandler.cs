using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using DotNetCore.CAP;

namespace Career.CAP.DomainEvent
{
    public abstract class CAPDomainEventHandler<TEvent> : IDomainEventHandler<TEvent>, ICapSubscribe where TEvent : IDomainEvent
    {
        public abstract Task Handle(TEvent domainEvent);
    }
}