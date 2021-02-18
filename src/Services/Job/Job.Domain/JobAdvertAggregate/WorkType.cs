using Career.Domain.Entities;

namespace Job.Domain.JobAdvertAggregate
{
    public class WorkType: DomainEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}