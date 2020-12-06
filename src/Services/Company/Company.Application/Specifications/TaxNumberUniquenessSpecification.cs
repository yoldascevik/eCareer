using System;
using System.Linq.Expressions;
using Career.Domain.Specifications;
using Company.Domain.Repositories;
using Company.Domain.Rules.Company;
using Company.Domain.Values;

namespace Company.Application.Specifications
{
    public class TaxNumberUniquenessSpecification : Specification<TaxInfo>, ITaxNumberUniquenessSpecification
    {
        private readonly Guid _companyId;
        private readonly ICompanyRepository _companyRepository;
        
        public TaxNumberUniquenessSpecification(ICompanyRepository companyRepository, Guid companyId = default)
        {
            _companyRepository = companyRepository;
            _companyId = companyId;
        }

        public override Expression<Func<TaxInfo, bool>> ToExpression()
        { 
            return taxInfo => !_companyRepository.IsTaxNumberExistsAsync(taxInfo.TaxNumber, taxInfo.CountryId, _companyId).Result;
        }
    }
}