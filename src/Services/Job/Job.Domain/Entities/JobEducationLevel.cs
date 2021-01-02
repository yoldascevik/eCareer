using System;
using Career.Domain.Entities;

namespace Job.Domain.Entities
{
    public class JobEducationLevel: DomainEntity<Guid>
    {
        public Guid JobAdvertId { get; set; }
        public string EducationLevelId { get; set; }
        public JobAdvert JobAdvert { get; set; }
    }
}