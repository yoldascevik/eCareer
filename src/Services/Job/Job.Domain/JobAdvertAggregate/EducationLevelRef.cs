using Career.Domain.Entities;

namespace Job.Domain.JobAdvertAggregate
{
    public class EducationLevelRef: DomainEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}