using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.TagAggregate.Events;

public class TagNameChangedEvent: DomainEvent
{
    private TagNameChangedEvent(){} // for serialization

    public TagNameChangedEvent(Tag tag, string oldTagName)
    {
        Check.NotNull(tag, nameof(tag));
        Check.NotNullOrEmpty(oldTagName, nameof(oldTagName));

        Tag = tag;
        OldTagName = oldTagName;
    }
        
    public Tag Tag { get; private set; }
    public string OldTagName { get; private set; }
}