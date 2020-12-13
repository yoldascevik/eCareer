using System;
using Career.Shared.Timing;

namespace Career.Domain.DomainEvent
{
    public abstract class DomainEvent: IDomainEvent
    {
        protected DomainEvent()
        {
            EventId = Guid.NewGuid();
            OccurredOn = Clock.Now;
        }
        
        public Guid EventId { get; }
        public DateTime OccurredOn { get; }
    }
}