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
            string address)
        {
            CheckRule(new AddressMustHaveCountryIdRule(countryId));
            CheckRule(new AddressMustHaveCityIdRule(cityId));
            
            CountryId = countryId;
            DistrictId = districtId;
            CityId = cityId;
            Address = address;
        }
        
        public string CountryId { get; }
        public string CityId { get;  }
        public string DistrictId { get; }
        public string Address { get; }
    }
}