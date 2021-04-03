using Career.Domain.Entities;
using Career.Exceptions;

namespace Job.Domain.JobAggregate
{
    public class WorkTypeRef : DomainEntity
    {
        public string WorkTypeId { get; private init; }
        public string Name { get; private init; }

        public static WorkTypeRef Create(string id, string name)
        {
            Check.NotNullOrEmpty(id, nameof(id));
            Check.NotNullOrEmpty(name, nameof(name));
            
            return new WorkTypeRef() {WorkTypeId = id, Name = name};
        }
    }
}