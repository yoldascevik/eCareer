using Career.Domain;
using Career.Exceptions;

namespace Company.Domain.Rules.TaxInfo
{
    public class TaxNumberRequiredRule: IBusinessRule
    {
        private readonly string _taxNumber;

        public TaxNumberRequiredRule(string taxNumber)
        {
            _taxNumber = taxNumber;
        }

        public bool IsBroken() => string.IsNullOrEmpty(_taxNumber);
        
        public string Message => "Tax number is required.";
    }
}