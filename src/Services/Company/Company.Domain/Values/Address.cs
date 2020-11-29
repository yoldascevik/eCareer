using Career.Domain;
using Company.Domain.Rules.CompanyAddress;

namespace Company.Domain.Values
{
    public class CompanyAddress: ValueObject
    {
        public CompanyAddress(
            string countryId, 
            string cityId, 
            string districtId, 
            string address,
            IValidAddressSpecification validAddressSpecification)
        {
            CheckRule(new AddressMustHaveCountryIdRule(countryId));
            CheckRule(new AddressMustHaveCityIdRule(cityId));
            CheckRule(new AddressMustBeValidRule(this, validAddressSpecification));
            
            CountryId = countryId;
            CityId = cityId;
            DistrictId = districtId;
            Address = address;
        }
        
        public string CountryId { get; }
        public string CityId { get;  }
        public string DistrictId { get; }
        public string Address { get; }
    }
}