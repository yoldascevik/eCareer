using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAggregate.Events;

public class JobUpdatedEvent : DomainEvent
{
    private JobUpdatedEvent(){} // for serialization

    public JobUpdatedEvent(Job job)
    {
        Check.NotNull(job, nameof(job));
        JobId = job.Id;
    }

    public Guid JobId { get; private set; }
}