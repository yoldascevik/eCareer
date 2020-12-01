using Career.Domain;
using Company.Domain.Rules.TaxInfo;

namespace Company.Domain.Values
{
    public class TaxInfo : ValueObject
    {
        public TaxInfo(string taxNumber, string taxOffice)
        {
            CheckRule(new TaxNumberRequiredRule(taxNumber));
            CheckRule(new TaxOfficeRequiredRule(taxOffice));
            
            TaxNumber = taxNumber;
            TaxOffice = taxOffice;
        }

        public string TaxNumber { get; }
        public string TaxOffice { get; }
    }
}