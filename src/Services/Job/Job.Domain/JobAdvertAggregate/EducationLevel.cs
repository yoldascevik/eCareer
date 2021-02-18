using Career.Domain.Entities;

namespace Job.Domain.JobAdvertAggregate
{
    public class EducationLevel: DomainEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}