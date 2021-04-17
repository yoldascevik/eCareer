using Career.Domain;
using Company.Domain.Rules.TaxInfo;

namespace Company.Domain.Values
{
    public class TaxInfo : ValueObject
    {
        public string TaxNumber { get; }
        public string TaxOffice { get; }
        public string CountryId { get; }
        
        public static TaxInfo Create(string taxNumber, string taxOffice, string countryId)
        {
            return new TaxInfo(taxNumber, taxOffice, countryId);
        }
        
        private TaxInfo(string taxNumber, string taxOffice, string countryId)
        {
            CheckRule(new TaxNumberRequiredRule(taxNumber));
            CheckRule(new TaxOfficeRequiredRule(taxOffice));
            CheckRule(new TaxInfoMustHaveCountryIdRule(countryId));
            
            TaxNumber = taxNumber;
            TaxOffice = taxOffice;
            CountryId = countryId;
        }
    }
}