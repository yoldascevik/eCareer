using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAggregate.Events
{
    public class JobCreatedEvent: DomainEvent
    {
        private JobCreatedEvent(){} // for serialization
        
        public JobCreatedEvent(Job job)
        {
            Check.NotNull(job, nameof(job));

            JobId = job.Id;
            Title = job.Title;
            CompanyId = job.CompanyId;
        }

        public Guid JobId { get; private set; }
        public Guid CompanyId { get; private set; }
        public string Title { get; private set; }
    }
}