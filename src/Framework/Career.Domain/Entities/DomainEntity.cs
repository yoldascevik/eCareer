using System.Collections.Generic;
using Career.Domain.BusinessRule;
using Career.Domain.DomainEvent;

namespace Career.Domain.Entities
{
    public class DomainEntity<TKey> : DomainEntity, IDomainEntity<TKey>
    {
        public TKey Id { get; set; }
    }
    
    public class DomainEntity: Entity, IDomainEntity
    {
        private List<IDomainEvent> _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        void IDomainEntity.AddDomainEvent(IDomainEvent domainEvent)
        {
            AddDomainEvent(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

        void IDomainEntity.CheckRule(IBusinessRule rule)
        {
            CheckRule(rule);
        }
    }
}