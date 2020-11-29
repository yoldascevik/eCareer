using Career.Domain;
using Career.Exceptions;

namespace Company.Domain.Rules.Company
{
    internal class CompanyTaxNumberMustBeUniqueRule : IBusinessRule
    {
        private readonly Values.TaxInfo _taxInfo;
        private readonly ICompanyTaxNumberUniquenessSpecification _companyTaxNumberUniquenessSpecification;

        public CompanyTaxNumberMustBeUniqueRule(
            Values.TaxInfo taxInfo,
            ICompanyTaxNumberUniquenessSpecification companyTaxNumberUniquenessSpecification)
        {
            Check.NotNull(taxInfo, nameof(taxInfo));
            Check.NotNull(companyTaxNumberUniquenessSpecification, nameof(companyTaxNumberUniquenessSpecification));
            
            _companyTaxNumberUniquenessSpecification = companyTaxNumberUniquenessSpecification;
            _taxInfo = taxInfo;
        }

        public bool IsBroken() => !_companyTaxNumberUniquenessSpecification.IsSatisfiedBy(_taxInfo);

        public string Message => "Company with this tax number already exists.";
    }
}