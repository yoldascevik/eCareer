using Career.Domain.BusinessRule;
using Career.Exceptions;

namespace Company.Domain.Rules.Company
{
    internal class CompanyTaxNumberMustBeUniqueRule : IBusinessRule
    {
        private readonly Entities.Company _company;
        private readonly ICompanyTaxNumberUniquenessSpecification _companyTaxNumberUniquenessSpecification;

        public CompanyTaxNumberMustBeUniqueRule(
            Entities.Company company,
            ICompanyTaxNumberUniquenessSpecification companyTaxNumberUniquenessSpecification)
        {
            Check.NotNull(company, nameof(company));
            Check.NotNull(companyTaxNumberUniquenessSpecification, nameof(companyTaxNumberUniquenessSpecification));
            
            _company = company;
            _companyTaxNumberUniquenessSpecification = companyTaxNumberUniquenessSpecification;
        }

        public bool IsBroken() => !_companyTaxNumberUniquenessSpecification.IsSatisfiedBy(_company);

        public string Message => "Company with this tax number already exists.";
    }
}