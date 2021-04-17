using Bogus;
using Company.Application.Specifications;
using Company.Domain.Repositories;
using Company.Domain.ValueObjects;
using Company.Domain.Values;
using NSubstitute;

namespace Company.Tests.Helpers
{
    public class CompanyFaker
    {
        public static Domain.Entities.Company CreateFakeCompany(bool isTaxNumberUnique = true, bool isEmailUnique = true)
        {
            var faker = new Faker();
            var countryId = faker.Random.Guid().ToString();
            var companyEmail = faker.Internet.Email();
            var companyRepository = Substitute.For<ICompanyRepository>();

            var taxInfo = TaxInfo.Create(faker.Company.TaxNumber(), faker.Address.City(), countryId);
            var sector = IdNameLookup.Create(faker.Random.Guid().ToString(), faker.Random.Word());
            var address = AddressInfo.Create(countryId, faker.Random.Guid().ToString(), faker.Random.Guid().ToString(), faker.Address.FullAddress());

            companyRepository.IsTaxNumberExistsAsync(taxInfo.TaxNumber, taxInfo.CountryId).Returns(!isTaxNumberUnique);
            companyRepository.IsCompanyEmailExists(companyEmail).Returns(!isEmailUnique);
            
            var taxNumberUniquenessSpec = new TaxNumberUniquenessSpecification(companyRepository);
            var emailUniquenessSpec = new EmailAddressUniquenessSpecification(companyRepository);
            
            var company = Domain.Entities.Company.Create( faker.Company.CompanyName(), companyEmail, taxInfo,
                address, faker.Phone.PhoneNumber(), sector, taxNumberUniquenessSpec, emailUniquenessSpec);

            return company;
        }
    }
}