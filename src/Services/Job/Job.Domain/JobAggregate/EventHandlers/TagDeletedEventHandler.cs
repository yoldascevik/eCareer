using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using Job.Domain.JobAggregate.Repositories;
using Job.Domain.TagAggregate.Events;
using Microsoft.Extensions.Logging;

namespace Job.Domain.JobAggregate.EventHandlers
{
    public class TagDeletedEventHandler : IDomainEventHandler<TagDeletedEvent>
    {
        private readonly ILogger<TagDeletedEventHandler> _logger;
        private readonly IJobRepository _jobRepository;

        public TagDeletedEventHandler(IJobRepository jobRepository, ILogger<TagDeletedEventHandler> logger)
        {
            _jobRepository = jobRepository;
            _logger = logger;
        }

        public async Task Handle(TagDeletedEvent notification, CancellationToken cancellationToken)
        {
            IEnumerable<Job> jobsOfTag = await _jobRepository.GetByTag(notification.Tag);
            if (!jobsOfTag.Any())
                return;

            foreach (Job job in jobsOfTag)
            {
                job.RemoveTag(notification.Tag);
                await _jobRepository.UpdateAsync(job.Id, job);

                _logger.LogInformation("Tag \"{TagName}\" removed from job : \"{JobId}\", after tag deleted", notification.Tag.Name, job.Id);
            }
        }
    }
}