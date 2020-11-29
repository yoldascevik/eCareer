using Career.Domain;
using Company.Domain.Rules.TaxInfo;

namespace Company.Domain.Values
{
    public class TaxInfo : ValueObject
    {
        public TaxInfo(string taxNumber, string taxOffice, string countryId)
        {
            CheckRule(new TaxNumberRequiredRule(taxNumber));
            CheckRule(new TaxOfficeRequiredRule(taxOffice));
            CheckRule(new TaxNumberMustHaveCountryIdRule(countryId));
            
            TaxNumber = taxNumber;
            TaxOffice = taxOffice;
            CountryId = countryId;
        }

        public string TaxNumber { get; }
        public string TaxOffice { get; }
        public string CountryId { get; }
    }
}