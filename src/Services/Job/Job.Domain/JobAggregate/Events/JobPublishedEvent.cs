using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAggregate.Events;

public class JobPublishedEvent : DomainEvent
{
    private JobPublishedEvent(){} // for serialization
        
    public JobPublishedEvent(Job job)
    {
        Check.NotNull(job, nameof(job));
        JobId = job.Id;
    }

    public Guid JobId { get; private set; }
}