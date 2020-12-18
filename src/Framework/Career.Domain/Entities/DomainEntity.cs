using System.Collections.Generic;
using Career.Domain.BusinessRule;
using Career.Domain.DomainEvent;

namespace Career.Domain.Entities
{
    public abstract class DomainEntity<TKey> : DomainEntity, IDomainEntity<TKey>
    {
        public TKey Id { get; protected set; }
    }
    
    public abstract class DomainEntity: Entity, IDomainEntity
    {
        private List<IDomainEvent> _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }
        
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
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