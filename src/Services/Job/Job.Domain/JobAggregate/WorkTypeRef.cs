using Career.Domain.Entities;

namespace Job.Domain.JobAggregate
{
    public class WorkTypeRef: DomainEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}