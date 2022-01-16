namespace Career.EventHub;

public interface IEventDispatcher
{
    Task Dispatch(IEvent @event, CancellationToken cancellationToken = default);
    Task Dispatch(IEnumerable<IEvent> events, CancellationToken cancellationToken = default);
}