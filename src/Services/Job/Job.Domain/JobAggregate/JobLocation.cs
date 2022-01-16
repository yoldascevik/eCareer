using System;
using Career.Domain.Entities;
using Career.Exceptions;
using Job.Domain.JobAggregate.Refs;

namespace Job.Domain.JobAggregate;

public class JobLocation : DomainEntity
{
    public Guid Id { get; private init; }
    public CountryRef Country { get; private set; }
    public CityRef City { get; private set; }

    public static JobLocation Create(CountryRef country, CityRef city)
    {
        return new JobLocation() {Id = Guid.NewGuid()}
            .SetCountry(country)
            .SetCity(city);
    }

    public JobLocation SetCountry(CountryRef country)
    {
        Check.NotNull(country, nameof(country));
        Country = country;

        return this;
    }

    public JobLocation SetCity(CityRef city)
    {
        Check.NotNull(city, nameof(city));
        City = city;

        return this;
    }
}