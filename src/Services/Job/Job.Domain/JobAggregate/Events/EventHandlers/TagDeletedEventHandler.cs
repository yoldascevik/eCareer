using System.Threading;
using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using Job.Domain.JobAggregate.Services;
using Job.Domain.TagAggregate.Events;

namespace Job.Domain.JobAggregate.Events.EventHandlers
{
    public class TagDeletedEventHandler : IDomainEventHandler<TagDeletedEvent>
    {
        private readonly IJobDomainService _jobDomainService;

        public TagDeletedEventHandler(IJobDomainService jobDomainService)
        {
            _jobDomainService = jobDomainService;
        }

        public async Task Handle(TagDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _jobDomainService.RemoveTagsFromJobsAsync(notification.Tag);
        }
    }
}