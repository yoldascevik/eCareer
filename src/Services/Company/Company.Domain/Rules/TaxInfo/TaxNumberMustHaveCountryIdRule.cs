using Career.Domain;
using Career.Exceptions;

namespace Company.Domain.Rules.TaxInfo
{
    public class TaxNumberMustHaveCountryIdRule: IBusinessRule
    {
        private readonly string _countryId;

        public TaxNumberMustHaveCountryIdRule(string countryId)
        {
            _countryId = countryId;
        }

        public bool IsBroken() => string.IsNullOrEmpty(_countryId);
        
        public string Message => "Tax number must be have country id.";
    }
}