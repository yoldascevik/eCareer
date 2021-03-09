using System.Threading;
using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using Job.Domain.JobAdvertAggregate.Events;
using Job.Domain.JobApplicationAggregate.Repositories;

namespace Job.Domain.JobApplicationAggregate.EventHandlers
{
    public class JobApplicationReceivedEventHandler : IDomainEventHandler<JobApplicationReceivedEvent>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public JobApplicationReceivedEventHandler(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task Handle(JobApplicationReceivedEvent notification, CancellationToken cancellationToken)
        {
            await _jobApplicationRepository.AddAsync(notification.JobApplication);
        }
    }
}