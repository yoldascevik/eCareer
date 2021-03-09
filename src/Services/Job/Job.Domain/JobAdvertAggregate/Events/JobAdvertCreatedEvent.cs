using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAdvertAggregate.Events
{
    public class JobAdvertCreatedEvent: DomainEvent
    {
        public JobAdvertCreatedEvent(JobAdvert jobAdvert)
        {
            Check.NotNull(jobAdvert, nameof(jobAdvert));
            JobAdvert = jobAdvert;
        }

        public JobAdvert JobAdvert { get; }
    }
}