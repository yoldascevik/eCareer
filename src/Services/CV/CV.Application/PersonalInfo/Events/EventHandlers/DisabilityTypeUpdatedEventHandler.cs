using System.Threading.Tasks;
using Career.CAP.DomainEvent;
using Career.Exceptions;
using CurriculumVitae.Core.Events;
using CurriculumVitae.Core.Repositories;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.PersonalInfo.Events.EventHandlers;

public class DisabilityTypeUpdatedEventHandler : CAPDomainEventHandler<DisabilityTypeUpdatedEvent>
{
    private readonly ICVRepository _cvRepository;
    private readonly ILogger<DisabilityTypeUpdatedEventHandler> _logger;
        
    public DisabilityTypeUpdatedEventHandler(ICVRepository cvRepository, ILogger<DisabilityTypeUpdatedEventHandler> logger)
    {
        _cvRepository = cvRepository;
        _logger = logger;
    }
        
    [CapSubscribe(nameof(DisabilityTypeUpdatedEvent))]
    public override async Task Handle(DisabilityTypeUpdatedEvent domainEvent)
    {
        Check.NotNull(domainEvent, nameof(domainEvent));

        if (!domainEvent.DisabilityType.IsDeleted)
        {
            await _cvRepository.UpdateAllDisabilityTypeNamesInCV(domainEvent.DisabilityType);
            _logger.LogInformation("Disability type name {Name} updated in all cvs", domainEvent.DisabilityType.Name);
        }
    }
}