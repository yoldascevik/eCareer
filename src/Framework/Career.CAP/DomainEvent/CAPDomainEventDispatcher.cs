using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using Career.Domain.DomainEvent.Dispatcher;
using DotNetCore.CAP;

namespace Career.CAP.DomainEvent
{
    public class CAPDomainEventDispatcher: IDomainEventDispatcher
    {
        private readonly ICapPublisher _publisher;
        
        public CAPDomainEventDispatcher(ICapPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task Dispatch(IDomainEvent @event, CancellationToken cancellationToken = default)
        {
            await _publisher.PublishAsync(@event.GetType().Name, @event, cancellationToken: cancellationToken);
        }
        
        public async Task Dispatch(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default)
        {
            IEnumerable<Task> tasks = events.Select(e => Dispatch(e, cancellationToken));
            await Task.WhenAll(tasks);
        }
    }
}