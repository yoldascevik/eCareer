using Career.CAP.DomainEvent;
using Career.Exceptions;
using CurriculumVitae.Core.Events;
using CurriculumVitae.Core.Repositories;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.PersonalInfo.Events.EventHandlers;

public class SocialProfileTypeUpdatedEventHandler : CAPDomainEventHandler<SocialProfileTypeUpdatedEvent>
{
    private readonly ICVRepository _cvRepository;
    private readonly ILogger<SocialProfileTypeUpdatedEventHandler> _logger;
        
    public SocialProfileTypeUpdatedEventHandler(ICVRepository cvRepository, ILogger<SocialProfileTypeUpdatedEventHandler> logger)
    {
        _cvRepository = cvRepository;
        _logger = logger;
    }
        
    [CapSubscribe(nameof(SocialProfileTypeUpdatedEvent))]
    public override async Task Handle(SocialProfileTypeUpdatedEvent domainEvent)
    {
        Check.NotNull(domainEvent, nameof(domainEvent));

        if (domainEvent.OldSocialProfileType.Name != domainEvent.NewSocialProfileType.Name 
            || domainEvent.OldSocialProfileType.ProfileUrlPrefix != domainEvent.NewSocialProfileType.ProfileUrlPrefix )
        {
            await _cvRepository.UpdateAllSocialProfileTypesInCV(domainEvent.NewSocialProfileType);
            _logger.LogInformation("Social profile type {Name} updated in all cvs", domainEvent.OldSocialProfileType.Name);
        }
    }
}