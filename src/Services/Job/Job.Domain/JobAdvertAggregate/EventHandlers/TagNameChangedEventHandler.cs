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
    public class TagNameChangedEventHandler: IDomainEventHandler<TagNameChangedEvent>
    {
        private readonly IJobAdvertRepository _jobAdvertRepository;
        private readonly ILogger<TagNameChangedEventHandler> _logger;

        public TagNameChangedEventHandler(IJobAdvertRepository jobAdvertRepository, ILogger<TagNameChangedEventHandler> logger)
        {
            _jobAdvertRepository = jobAdvertRepository;
            _logger = logger;
        }

        public async Task Handle(TagNameChangedEvent notification, CancellationToken cancellationToken)
        {
            IEnumerable<JobAdvert> jobAdvertsOfTag = await _jobAdvertRepository.GetByTag(notification.Tag);
            if (!jobAdvertsOfTag.Any())
                return;

            foreach (JobAdvert jobAdvert in jobAdvertsOfTag)
            {
                jobAdvert.RemoveTag(notification.Tag);
                jobAdvert.AddTag(notification.Tag);
                await _jobAdvertRepository.UpdateAsync(jobAdvert.Id, jobAdvert);

                _logger.LogInformation("Tag \"{OldTagName}\" replaced to \"{NewTagName}\" for job advert : \"{JobAdvertId}", 
                    notification.OldTagName, 
                    notification.Tag.Name, 
                    jobAdvert.Id);
            }
        }
    }
}