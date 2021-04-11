using System.Threading.Tasks;
using Career.CAP.DomainEvent;
using Career.Domain.DomainEvent;
using DotNetCore.CAP;
using Job.Domain.JobAggregate.Services;
using Job.Domain.TagAggregate.Events;

namespace Job.Domain.JobAggregate.Events.EventHandlers
{
    public class TagDeletedEventHandler : CAPDomainEventHandler<TagDeletedEvent>
    {
        private readonly IJobDomainService _jobDomainService;

        public TagDeletedEventHandler(IJobDomainService jobDomainService)
        {
            _jobDomainService = jobDomainService;
        }

        [CapSubscribe(nameof(TagDeletedEvent))]
        public override async Task Handle(TagDeletedEvent domainEvent)
        {
            await _jobDomainService.RemoveTagsFromJobsAsync(domainEvent.Tag);
        }
    }
}