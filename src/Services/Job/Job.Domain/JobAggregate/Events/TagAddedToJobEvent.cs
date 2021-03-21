using Career.Domain.DomainEvent;
using Career.Exceptions;
using Job.Domain.TagAggregate;

namespace Job.Domain.JobAggregate.Events
{
    public class TagAddedToJobEvent : DomainEvent
    {
        public TagAddedToJobEvent(Job job, Tag tag)
        {
            Check.NotNull(job, nameof(job));
            Check.NotNull(tag, nameof(tag));

            Job = job;
            Tag = tag;
        }

        public Job Job { get; }
        public Tag Tag { get; }    
    }
}