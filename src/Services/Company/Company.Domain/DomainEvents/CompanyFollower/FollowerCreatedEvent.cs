using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Company.Domain.DomainEvents.CompanyFollower;

public class FollowerCreatedEvent : DomainEvent
{
    private FollowerCreatedEvent(){} // for serialization

    public FollowerCreatedEvent(Entities.CompanyFollower follower)
    {
        Check.NotNull(follower, nameof(follower));

        UserId = follower.UserId;
        CompanyId = follower.CompanyId;
        CompanyName = follower.Company.Name;
        CompanyEmailAddress = follower.Company.Email;
    }

    public Guid UserId { get; private set; }
    public Guid CompanyId { get; private set; }
    public string CompanyName { get; private set; }
    public string CompanyEmailAddress { get; private set; }
}