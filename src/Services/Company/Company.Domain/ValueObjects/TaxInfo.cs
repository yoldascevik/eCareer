using Career.Domain;
using Company.Domain.Rules.TaxInfo;

namespace Company.Domain.ValueObjects
{
    public class TaxInfo : ValueObject
    {
        public string TaxNumber { get; }
        public string TaxOffice { get; }
        public string TaxCountryId { get; }
        
        public static TaxInfo Create(string taxNumber, string taxOffice, string taxCountryId)
        {
            return new TaxInfo(taxNumber, taxOffice, taxCountryId);
        }
        
        private TaxInfo(string taxNumber, string taxOffice, string taxCountryId)
        {
            CheckRule(new TaxNumberRequiredRule(taxNumber));
            CheckRule(new TaxOfficeRequiredRule(taxOffice));
            CheckRule(new TaxInfoMustHaveCountryIdRule(taxCountryId));
            
            TaxNumber = taxNumber;
            TaxOffice = taxOffice;
            TaxCountryId = taxCountryId;
        }
    }
}