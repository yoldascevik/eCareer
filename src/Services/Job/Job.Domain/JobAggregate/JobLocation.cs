using System;
using Career.Domain.Entities;
using Career.Exceptions;

namespace Job.Domain.JobAggregate
{
    public class JobLocation : DomainEntity
    {
        public Guid Id { get; private init; }
        public IdNameRef CountryRef { get; private set; }
        public IdNameRef CityRef { get; private set; }

        public static JobLocation Create(IdNameRef countryRef, IdNameRef cityRef)
        {
            return new JobLocation() {Id = Guid.NewGuid()}
                .SetCountry(countryRef)
                .SetCity(cityRef);
        }

        public JobLocation SetCountry(IdNameRef countryRef)
        {
            Check.NotNull(countryRef, nameof(countryRef));
            CountryRef = countryRef;

            return this;
        }

        public JobLocation SetCity(IdNameRef cityRef)
        {
            Check.NotNull(cityRef, nameof(cityRef));
            CityRef = cityRef;

            return this;
        }
    }
}