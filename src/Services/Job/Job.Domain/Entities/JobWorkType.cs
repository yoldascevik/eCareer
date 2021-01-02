using System;
using Career.Domain.Entities;

namespace Job.Domain.Entities
{
    public class JobWorkType: DomainEntity<Guid>
    {
        public Guid JobAdvertId { get; set; }
        public string WorkTypeId { get; set; }
        public JobAdvert JobAdvert { get; set; }
    }
}