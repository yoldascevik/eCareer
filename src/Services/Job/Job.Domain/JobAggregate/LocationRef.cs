using Career.Domain.Entities;

namespace Job.Domain.JobAggregate
{
    public class LocationRef: DomainEntity
    {
        public string Id { get; set; }
        public string CountryId { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
    }
}