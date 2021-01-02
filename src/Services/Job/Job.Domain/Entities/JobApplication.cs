using System;
using Career.Domain.Entities;

namespace Job.Domain.Entities
{
    public class JobApplication: DomainEntity<Guid>
    {
        public Guid JobAdvertId { get; set; }
        public Guid UserId { get; set; }
        public Guid CvId { get; set; }
        public string CoverLetter { get; set; }
        public string Channel { get; set; }
        public string Referance { get; set; }
        public DateTime ApplicationDate { get; set; }
        public bool IsDeleted { get; set; }
        public JobAdvert JobAdvert { get; set; }
    }
}