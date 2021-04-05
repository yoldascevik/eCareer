using System.Threading;
using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using Job.Domain.JobAggregate.Services;
using Job.Domain.TagAggregate.Events;

namespace Job.Domain.JobAggregate.Events.EventHandlers
{
    public class TagNameChangedEventHandler: IDomainEventHandler<TagNameChangedEvent>
    {
        private readonly IJobDomainService _jobDomainService;

        public TagNameChangedEventHandler(IJobDomainService jobDomainService)
        {
            _jobDomainService = jobDomainService;
        }

        public async Task Handle(TagNameChangedEvent notification, CancellationToken cancellationToken)
        {
            await _jobDomainService.UpdateTagNameFromJobsAsync(notification.Tag);
        }
    }
}