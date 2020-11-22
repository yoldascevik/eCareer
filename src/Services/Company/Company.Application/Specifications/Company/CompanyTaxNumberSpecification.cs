using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Company.Domain.Repository;

namespace Company.Application.Specifications.Company
{
    public class CompanyTaxNumberSpecification: ICompanyTaxNumberSpecification
    {
        private readonly ICompanyRepository _companyRepository;
            
        public CompanyTaxNumberSpecification(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        
        public async Task<bool> IsSatisfiedByAsync(Domain.Company company)
        {
            if (await _companyRepository.IsTaxNumberExistsAsync(company.TaxNumber, company.CountryId, company.Id))
                throw new ItemAlreadyExistsException($"TaxNumber already registered in system: {company.TaxNumber}");

            return true;
        }
    }
}