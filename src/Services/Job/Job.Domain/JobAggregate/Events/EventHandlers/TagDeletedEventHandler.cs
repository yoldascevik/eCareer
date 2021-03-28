using System.Threading;
using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using Job.Domain.JobAggregate.Repositories;
using Job.Domain.TagAggregate.Events;
using Microsoft.Extensions.Logging;

namespace Job.Domain.JobAggregate.Events.EventHandlers
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
            await _jobRepository.RemoveTagsFromJobsAsync(notification.Tag);
            
            _logger.LogInformation("Tag \"{TagName}\" removed from jobs, after tag deleted", notification.Tag.Name);
        }
    }
}