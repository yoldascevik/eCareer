using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAggregate.Events
{
    public class JobRevokedEvent : DomainEvent
    {
        private JobRevokedEvent(){} // for serialization

        public JobRevokedEvent(Job job)
        {
            Check.NotNull(job, nameof(job));
            JobId = job.Id;
        }

        public Guid JobId { get; private set; }
    }
}