using System;
using Career.Domain.Entities;

namespace Job.Domain.Entities
{
    public class JobViewingHistory: DomainEntity<Guid>
    {
        public Guid JobAdvertId { get; set; }
        public Guid UserId { get; set; }
        public DateTime ViewingDate { get; set; }
        public string Channel { get; set; }
        public string Referance { get; set; }
        public JobAdvert JobAdvert { get; set; }
    }
}