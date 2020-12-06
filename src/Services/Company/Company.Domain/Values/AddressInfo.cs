using Career.Domain;
using Company.Domain.Rules.CompanyAddress;

namespace Company.Domain.Values
{
    public class AddressInfo : ValueObject
    {
        public string CountryId { get; private set; }
        public string CityId { get; private set; }
        public string DistrictId { get; private set; }
        public string Address { get; private set; }

        public static AddressInfo Create(
            string countryId,
            string cityId,
            string districtId,
            string address)
        {
            CheckRule(new AddressMustHaveCountryIdRule(countryId));
            CheckRule(new AddressMustHaveCityIdRule(cityId));

            return new AddressInfo()
            {
                CountryId = countryId,
                DistrictId = districtId,
                CityId = cityId,
                Address = address
            };
        }
    }
}