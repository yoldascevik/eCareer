using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;
using Job.Domain.TagAggregate;

namespace Job.Domain.JobAggregate.Events
{
    public class TagAddedToJobEvent : DomainEvent
    {
        private TagAddedToJobEvent(){} // for serialization

        public TagAddedToJobEvent(Job job, Tag tag)
        {
            Check.NotNull(job, nameof(job));
            Check.NotNull(tag, nameof(tag));

            JobId = job.Id;
            Tag = tag;
        }

        public Guid JobId { get; private set; }
        public Tag Tag { get; private set; }    
    }
}