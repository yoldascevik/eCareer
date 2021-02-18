using Career.Domain.DomainEvent;
using Career.Exceptions;
using Job.Domain.JobAdvertAggregate;

namespace Job.Domain.JobApplicationAggregate.DomainEvents
{
    public class ApplicationCreatedEvent : DomainEvent
    {
        public ApplicationCreatedEvent(JobAdvert jobAdvert, JobApplication jobApplication, ApplicationEventSource eventSource)
        {
            Check.NotNull(jobAdvert, nameof(jobAdvert));
            Check.NotNull(jobApplication, nameof(jobApplication));

            JobAdvert = jobAdvert;
            JobApplication = jobApplication;
            EventSource = eventSource;
        }

        public JobAdvert JobAdvert { get; }
        public JobApplication JobApplication { get; }
        public ApplicationEventSource EventSource { get; }
    }
}