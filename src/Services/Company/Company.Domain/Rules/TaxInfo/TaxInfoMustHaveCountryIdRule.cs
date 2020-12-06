using Career.Domain.BusinessRule;

namespace Company.Domain.Rules.TaxInfo
{
    public class TaxInfoMustHaveCountryIdRule : IBusinessRule
    {
        private readonly string _countryId;

        public TaxInfoMustHaveCountryIdRule(string countryId)
        {
            _countryId = countryId;
        }

        public bool IsBroken() => string.IsNullOrEmpty(_countryId);

        public string Message => "Tax info must be have country id.";
    }
}