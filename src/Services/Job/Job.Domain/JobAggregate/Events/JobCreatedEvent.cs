using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;
using Job.Domain.JobAggregate.Refs;

namespace Job.Domain.JobAggregate.Events;

public class JobCreatedEvent: DomainEvent
{
    private JobCreatedEvent(){} // for serialization
        
    public JobCreatedEvent(Job job)
    {
        Check.NotNull(job, nameof(job));

        JobId = job.Id;
        Title = job.Title;
        Company = job.Company;
    }

    public Guid JobId { get; private set; }
    public CompanyRef Company { get; private set; }
    public string Title { get; private set; }
}