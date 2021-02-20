using Career.Domain.DomainEvent;
using Career.Exceptions;
using Job.Domain.JobApplicationAggregate;

namespace Job.Domain.JobAdvertAggregate.DomainEvents
{
    public class JobApplicationReceivedEvent : DomainEvent
    {
        public JobApplicationReceivedEvent(JobAdvert jobAdvert, JobApplication jobApplication)
        {
            Check.NotNull(jobAdvert, nameof(jobAdvert));
            Check.NotNull(jobApplication, nameof(jobApplication));

            JobAdvert = jobAdvert;
            JobApplication = jobApplication;
        }

        public JobAdvert JobAdvert { get; }
        public JobApplication JobApplication { get; }
    }
}