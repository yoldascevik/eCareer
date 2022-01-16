namespace Career.EventHub;

public interface IEvent
{
    Guid EventId { get; }
    DateTime OccurredOn { get; }
}