using Career.Domain.DomainEvent;
using CurriculumVitae.Core.Entities;

namespace CurriculumVitae.Core.Events;

public class SocialProfileTypeUpdatedEvent : DomainEvent
{
    private SocialProfileTypeUpdatedEvent(){} // for serialization

    public SocialProfileTypeUpdatedEvent(SocialProfileType oldSocialProfileType)
    {
        OldSocialProfileType = oldSocialProfileType;
    }
        
    public SocialProfileType OldSocialProfileType { get; private set; }
    public SocialProfileType NewSocialProfileType { get; set; }
}