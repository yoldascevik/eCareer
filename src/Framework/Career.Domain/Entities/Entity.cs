using Career.Domain.BusinessRule;

namespace Career.Domain.Entities
{
    public class Entity<TKey> : Entity, IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
    
    public class Entity: IEntity
    {
        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}