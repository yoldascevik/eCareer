using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Company.Domain.DomainEvents.CompanyFollower
{
    public class FollowerCreatedEvent : DomainEvent
    {
        public FollowerCreatedEvent(Entities.CompanyFollower follower)
        {
            Check.NotNull(follower, nameof(follower));

            UserId = follower.UserId;
            CompanyId = follower.CompanyId;
            CompanyName = follower.Company.Name;
            CompanyEmailAddress = follower.Company.Email;
        }

        public Guid UserId { get; }
        public Guid CompanyId { get; }
        public string CompanyName { get; }
        public string CompanyEmailAddress { get; }
    }
}