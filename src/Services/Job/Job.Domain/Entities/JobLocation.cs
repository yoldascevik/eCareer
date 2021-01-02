using System;
using Career.Domain.Entities;

namespace Job.Domain.Entities
{
    public class JobLocation: DomainEntity<Guid>
    {
        public Guid JobAdvertId { get; set; }
        public string CountryId { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
        public JobAdvert JobAdvert { get; set; }
    }
}