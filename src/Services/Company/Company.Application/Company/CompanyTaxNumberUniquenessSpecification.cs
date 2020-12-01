using Company.Domain.Repositories;
using Company.Domain.Rules.Company;

namespace Company.Application.Company
{
    public class CompanyTaxNumberUniquenessSpecification: ICompanyTaxNumberUniquenessSpecification
    {
        private readonly ICompanyRepository _companyRepository;
        
        public CompanyTaxNumberUniquenessSpecification(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        
        public bool IsSatisfiedBy(Domain.Entities.Company company)
        {
            return true;
            // TODO: Async specification implemantation
            //return await _companyRepository.IsTaxNumberExistsAsync(company.TaxInfo.TaxNumber, company.Address.CountryId, company.Id);
        }
    }
}