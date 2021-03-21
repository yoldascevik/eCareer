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
            await _jobRepository.UpdateTagNameFromJobsAsync(notification.Tag);

            _logger.LogInformation("Tag \"{OldTagName}\" replaced to \"{NewTagName}\" in jobs", notification.OldTagName, notification.Tag.Name);
        }
    }
}