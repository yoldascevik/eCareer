using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.TagAggregate.Events
{
    public class TagNameChangedEvent: DomainEvent
    {
        public TagNameChangedEvent(Tag tag, string oldTagName)
        {
            Check.NotNull(tag, nameof(tag));
            Check.NotNullOrEmpty(oldTagName, nameof(oldTagName));
            
            Tag = tag;
            OldTagName = oldTagName;
        }
        
        public Tag Tag { get; }
        public string OldTagName { get; set; }
    }
}