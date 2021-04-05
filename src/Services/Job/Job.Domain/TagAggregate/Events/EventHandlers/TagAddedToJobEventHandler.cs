using System.Threading;
using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using Job.Domain.JobAggregate.Events;
using Job.Domain.TagAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace Job.Domain.TagAggregate.Events.EventHandlers
{
    public class TagAddedToJobEventHandler: IDomainEventHandler<TagAddedToJobEvent>
    {
        private readonly ITagRepository _tagRepository;
        private readonly ILogger<TagAddedToJobEventHandler> _logger;

        public TagAddedToJobEventHandler(ITagRepository tagRepository, ILogger<TagAddedToJobEventHandler> logger)
        {
            _tagRepository = tagRepository;
            _logger = logger;
        }

        public async Task Handle(TagAddedToJobEvent notification, CancellationToken cancellationToken)
        {
            if (await _tagRepository.Exists(notification.Tag.Name))
                return;

            await _tagRepository.AddAsync(notification.Tag);
            
            _logger.LogInformation("New tag added: {TagName}", notification.Tag.Name);
        }
    }
}