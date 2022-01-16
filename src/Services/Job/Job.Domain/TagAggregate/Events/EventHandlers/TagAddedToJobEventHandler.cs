using Career.CAP.DomainEvent;
using DotNetCore.CAP;
using Job.Domain.JobAggregate.Events;
using Job.Domain.TagAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace Job.Domain.TagAggregate.Events.EventHandlers;

public class TagAddedToJobEventHandler: CAPDomainEventHandler<TagAddedToJobEvent>
{
    private readonly ITagRepository _tagRepository;
    private readonly ILogger<TagAddedToJobEventHandler> _logger;

    public TagAddedToJobEventHandler(ITagRepository tagRepository, ILogger<TagAddedToJobEventHandler> logger)
    {
        _tagRepository = tagRepository;
        _logger = logger;
    }

    [CapSubscribe(nameof(TagAddedToJobEvent))]
    public override async Task Handle(TagAddedToJobEvent domainEvent)
    {
        if (await _tagRepository.Exists(domainEvent.Tag.Name))
            return;

        await _tagRepository.AddAsync(domainEvent.Tag);
            
        _logger.LogInformation("New tag added: {TagName}", domainEvent.Tag.Name);
    }
}