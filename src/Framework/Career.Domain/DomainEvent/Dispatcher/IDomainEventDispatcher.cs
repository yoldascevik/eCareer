using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Career.Domain.DomainEvent.Dispatcher
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
        Task Dispatch(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}