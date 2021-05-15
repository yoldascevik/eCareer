using System;
using Career.Shared.Timing;

namespace Career.EventHub
{
    public abstract class Event : IEvent
    {
        protected Event()
        {
            EventId = Guid.NewGuid();
            OccurredOn = Clock.Now;
        }

        public Guid EventId { get; }
        public DateTime OccurredOn { get; }
    }
}