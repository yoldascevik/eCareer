using System;
using System.Linq.Expressions;
using Career.Domain.Specifications;
using Company.Domain.Repositories;
using Company.Domain.Rules.Company;

namespace Company.Application.Company
{
    public class CompanyTaxNumberUniquenessSpecification: Specification<Domain.Entities.Company>, ICompanyTaxNumberUniquenessSpecification
    {
        private readonly ICompanyRepository _companyRepository;
        
        public CompanyTaxNumberUniquenessSpecification(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public override Expression<Func<Domain.Entities.Company, bool>> ToExpression()
        { 
            return company => _companyRepository.IsTaxNumberExistsAsync(company.TaxInfo.TaxNumber, company.Address.CountryId, company.Id).Result;
        }
    }
}