using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAggregate.Events;

public class JobSentForApprovalEvent : DomainEvent
{
    private JobSentForApprovalEvent(){} // for serialization

    public JobSentForApprovalEvent(Job job)
    {
        Check.NotNull(job, nameof(job));
        JobId = job.Id;
    }

    public Guid JobId { get; private set; }
}