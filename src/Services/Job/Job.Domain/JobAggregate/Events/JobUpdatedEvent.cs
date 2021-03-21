using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAggregate.Events
{
    public class JobUpdatedEvent : DomainEvent
    {
        public JobUpdatedEvent(Job job)
        {
            Check.NotNull(job, nameof(job));
            Job = job;
        }

        public Job Job { get; }
    }
}