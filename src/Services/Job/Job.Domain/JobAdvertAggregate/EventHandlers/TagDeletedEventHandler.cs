using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using Job.Domain.JobAdvertAggregate.Repositories;
using Job.Domain.TagAggregate.Events;
using Microsoft.Extensions.Logging;

namespace Job.Domain.JobAdvertAggregate.EventHandlers
{
    public class TagDeletedEventHandler : IDomainEventHandler<TagDeletedEvent>
    {
        private readonly ILogger<TagDeletedEventHandler> _logger;
        private readonly IJobAdvertRepository _jobAdvertRepository;

        public TagDeletedEventHandler(IJobAdvertRepository jobAdvertRepository, ILogger<TagDeletedEventHandler> logger)
        {
            _jobAdvertRepository = jobAdvertRepository;
            _logger = logger;
        }

        public async Task Handle(TagDeletedEvent notification, CancellationToken cancellationToken)
        {
            IEnumerable<JobAdvert> jobAdvertsOfTag = await _jobAdvertRepository.GetByTag(notification.Tag);
            if (!jobAdvertsOfTag.Any())
                return;

            foreach (JobAdvert jobAdvert in jobAdvertsOfTag)
            {
                jobAdvert.RemoveTag(notification.Tag);
                await _jobAdvertRepository.UpdateAsync(jobAdvert.Id, jobAdvert);

                _logger.LogInformation("Tag \"{TagName}\" removed from job advert : \"{JobAdvertId}\", after tag deleted", notification.Tag.Name, jobAdvert.Id);
            }
        }
    }
}