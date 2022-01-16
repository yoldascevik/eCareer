using Bogus;
using Company.Application.Company.Dtos;
using Company.Domain.Entities;
using Company.Domain.Refs;

namespace Company.Tests.Helpers;

public class AddressFaker
{
    public static Address GenerateCompanyAddress(Guid companyId, bool isPrimary = false)
    {
        var faker = new Faker();
        return Address.Create(
            companyId,
            faker.Lorem.Word(),
            CountryRef.Create(Guid.NewGuid().ToString(), faker.Address.Country()),
            CityRef.Create(Guid.NewGuid().ToString(), faker.Address.City()),
            DistrictRef.Create(Guid.NewGuid().ToString(), faker.Address.State()),
            faker.Address.FullAddress(),
            faker.Address.ZipCode(),
            isPrimary);
    }

    public static AddressInputDto GenerateAddressInputDto(bool isPrimary = false)
    {
        var faker = new Faker();
        return new AddressInputDto
        {
            Title = faker.Lorem.Word(),
            Details = faker.Address.FullAddress(),
            Country = new IdNameRefDto(Guid.NewGuid().ToString(), faker.Address.Country()),
            City = new IdNameRefDto(Guid.NewGuid().ToString(), faker.Address.City()),
            District = new IdNameRefDto(Guid.NewGuid().ToString(), faker.Address.State()),
            ZipCode = faker.Address.ZipCode(),
            IsPrimary = isPrimary
        };
    }
}