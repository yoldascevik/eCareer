using System;
using Career.Shared.Timing;

namespace Career.Domain.DomainEvent
{
    public class DomainEvent: IDomainEvent
    {
        public DomainEvent()
        {
            EventId = Guid.NewGuid();
            OccurredOn = Clock.Now;
        }
        
        public Guid EventId { get; }
        public DateTime OccurredOn { get; }
    }
}