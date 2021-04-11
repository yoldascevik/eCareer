using System.Threading.Tasks;

namespace Career.Domain.DomainEvent
{
    public interface IDomainEventHandler
    {
    }

    public interface IDomainEventHandler<TEvent> : IDomainEventHandler where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent);
    }
}