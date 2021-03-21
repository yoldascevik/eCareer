using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.TagAggregate.Events
{
    public class TagDeletedEvent : DomainEvent
    {
        public TagDeletedEvent(Tag tag)
        {
            Check.NotNull(tag, nameof(tag));
            Tag = tag;
        }

        public Tag Tag { get; }
    }
}