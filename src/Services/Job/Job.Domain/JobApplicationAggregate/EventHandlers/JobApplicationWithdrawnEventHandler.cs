using System.Threading;
using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using Career.Exceptions.Exceptions;
using Job.Domain.JobAdvertAggregate.Events;
using Job.Domain.JobApplicationAggregate.Repositories;

namespace Job.Domain.JobApplicationAggregate.EventHandlers
{
    public class JobApplicationWithdrawnEventHandler : IDomainEventHandler<JobApplicationWithdrawnEvent>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public JobApplicationWithdrawnEventHandler(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task Handle(JobApplicationWithdrawnEvent notification, CancellationToken cancellationToken)
        {
            JobApplication jobApplication = await _jobApplicationRepository.GetByIdAsync(notification.JobApplication.Id);
            if (jobApplication == null)
            {
                throw new NotFoundException($"JobApplication not found ({notification.JobApplication.Id}).");
            }

            jobApplication.Withdrawn();
            await _jobApplicationRepository.UpdateAsync(jobApplication.Id, jobApplication);
        }
    }
}