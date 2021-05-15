using Career.Domain.BusinessRule;
using Company.Domain.Refs;

namespace Company.Domain.Rules.CompanyAddress
{
    public class AddressMustHaveCountryRule : IBusinessRule
    {
        private readonly CountryRef _countryRef;

        public AddressMustHaveCountryRule(CountryRef countryRef)
        {
            _countryRef = countryRef;
        }

        public bool IsBroken() => _countryRef == null;

        public string Message => "Address must be have country information.";
    }
}