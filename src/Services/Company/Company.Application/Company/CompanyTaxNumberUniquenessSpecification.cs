using Company.Domain.Repositories;
using Company.Domain.Rules;
using Company.Domain.Rules.Company;
using Company.Domain.Values;

namespace Company.Application.Company
{
    public class CompanyTaxNumberUniquenessSpecification: ICompanyTaxNumberUniquenessSpecification
    {
        private readonly ICompanyRepository _companyRepository;
        
        public CompanyTaxNumberUniquenessSpecification(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        
        public bool IsSatisfiedBy(TaxInfo taxInfo)
        {
            return await _companyRepository.IsTaxNumberExistsAsync(taxInfo.TaxNumber, taxInfo.CountryId);
        }
    }
}