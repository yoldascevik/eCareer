using Career.Domain.DomainEvent;
using Career.Exceptions;
using CurriculumVitae.Core.Entities;

namespace CurriculumVitae.Core.Events
{
    public class DisabilityTypeUpdatedEvent : DomainEvent
    {
        private DisabilityTypeUpdatedEvent(){} // for serialization
        
        public DisabilityTypeUpdatedEvent(DisabilityType disabilityType) 
        {
            Check.NotNull(disabilityType, nameof(Entities.DisabilityType));
            DisabilityType = disabilityType;
        }

        public DisabilityType DisabilityType { get; private set; }
    }
}