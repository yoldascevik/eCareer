using Career.Domain.Entities;
using Career.Shared.Timing;

namespace Job.Domain.JobAggregate;

public class ViewingHistory: DomainEntity
{
    public Guid Id { get; private init; }
    public Guid UserId { get; private init; }
    public DateTime ViewingDate { get; private init; }
    public string Channel { get; private init; }
    public string Referance { get; private init; }

    public static ViewingHistory Create(Guid userId, DateTime viewingDate, string channel = null, string referance = null)
    {
        return new ViewingHistory()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ViewingDate = Clock.Normalize(viewingDate),
            Channel = channel,
            Referance = referance
        };
    }
}