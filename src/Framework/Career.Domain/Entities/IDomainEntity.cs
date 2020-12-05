using System.Collections.Generic;
using Career.Domain.BusinessRule;
using Career.Domain.DomainEvent;

namespace Career.Domain.Entities
{
    public interface IDomainEntity : IEntity
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void AddDomainEvent(IDomainEvent domainEvent);
        void ClearDomainEvents();
        void CheckRule(IBusinessRule rule);
    }

    public interface IDomainEntity<TKey> : IDomainEntity
    {
    }
}