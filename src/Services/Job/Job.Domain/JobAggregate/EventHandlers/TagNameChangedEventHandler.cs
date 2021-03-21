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
    public class TagNameChangedEventHandler: IDomainEventHandler<TagNameChangedEvent>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<TagNameChangedEventHandler> _logger;

        public TagNameChangedEventHandler(IJobRepository jobRepository, ILogger<TagNameChangedEventHandler> logger)
        {
            _jobRepository = jobRepository;
            _logger = logger;
        }

        public async Task Handle(TagNameChangedEvent notification, CancellationToken cancellationToken)
        {
            IEnumerable<Job> jobsOfTag = await _jobRepository.GetByTag(notification.Tag);
            if (!jobsOfTag.Any())
                return;

            foreach (Job job in jobsOfTag)
            {
                job.RemoveTag(notification.Tag);
                job.AddTag(notification.Tag);
                await _jobRepository.UpdateAsync(job.Id, job);

                _logger.LogInformation("Tag \"{OldTagName}\" replaced to \"{NewTagName}\" for job : \"{JobId}", 
                    notification.OldTagName, 
                    notification.Tag.Name, 
                    job.Id);
            }
        }
    }
}