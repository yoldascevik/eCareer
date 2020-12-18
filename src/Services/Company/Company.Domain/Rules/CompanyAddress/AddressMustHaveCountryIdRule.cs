using Career.Domain.BusinessRule;

namespace Company.Domain.Rules.CompanyAddress
{
    public class AddressMustHaveCountryIdRule : IBusinessRule
    {
        private readonly string _countryId;

        public AddressMustHaveCountryIdRule(string countryId)
        {
            _countryId = countryId;
        }

        public bool IsBroken() => string.IsNullOrEmpty(_countryId);

        public string Message => "Address must be have country id.";
    }
}