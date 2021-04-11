using System;
using Career.Domain.Entities;
using Career.Exceptions;

namespace Job.Domain.JobAggregate
{
    public class LocationRef : DomainEntity
    {
        public Guid Id { get; private init; }
        public string CountryId { get; private set; }
        public string CityId { get; private set; }
        public string DistrictId { get; private set; }

        public static LocationRef Create(string countryId, string cityId, string districtId)
        {
            return new LocationRef() {Id = Guid.NewGuid()}
                .SetCountry(countryId)
                .SetCity(cityId)
                .SetDistrict(districtId);
        }

        public LocationRef SetCountry(string countryId)
        {
            Check.NotNullOrEmpty(countryId, nameof(countryId));
            CountryId = countryId;

            return this;
        }

        public LocationRef SetCity(string cityId)
        {
            Check.NotNullOrEmpty(cityId, nameof(cityId));
            CityId = cityId;

            return this;
        }

        public LocationRef SetDistrict(string districtId)
        {
            DistrictId = districtId;
            return this;
        }
    }
}