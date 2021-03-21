using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAggregate.Events
{
    public class JobRevokedEvent : DomainEvent
    {
        public JobRevokedEvent(Job job)
        {
            Check.NotNull(job, nameof(job));
            Job = job;
        }

        public Job Job { get; }
    }
}