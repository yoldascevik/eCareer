using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Career.Domain.DomainEvent.Dispatcher
{
    public class DomainEventDispatcher: IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Dispatch(IDomainEvent @event, CancellationToken cancellationToken = default)
        {
            await _mediator.Publish(@event, cancellationToken);
        }

        public async Task Dispatch(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default)
        {
            IEnumerable<Task> tasks = events.Select(e => _mediator.Publish(e, cancellationToken));
            await Task.WhenAll(tasks);
        }
    }
}