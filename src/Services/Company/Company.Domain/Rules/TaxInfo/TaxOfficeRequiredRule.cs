using Career.Domain.BusinessRule;

namespace Company.Domain.Rules.TaxInfo
{
    public class TaxOfficeRequiredRule: IBusinessRule
    {
        private readonly string _taxOffice;

        public TaxOfficeRequiredRule(string taxOffice)
        {
            _taxOffice = taxOffice;
        }

        public bool IsBroken() => string.IsNullOrEmpty(_taxOffice);
        
        public string Message => "Tax office is required.";
    }
}