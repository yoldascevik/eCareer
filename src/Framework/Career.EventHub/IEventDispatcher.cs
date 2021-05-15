using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Career.EventHub
{
    public interface IEventDispatcher
    {
        Task Dispatch(IEvent @event, CancellationToken cancellationToken = default);
        Task Dispatch(IEnumerable<IEvent> events, CancellationToken cancellationToken = default);
    }
}