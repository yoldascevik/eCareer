using System;
using Career.MediatR.Query;
using Company.Application.Company.Dtos;

namespace Company.Application.Company.Queries.GetCompanyTaxInfo
{
    public class GetCompanyTaxInfoQuery: IQuery<TaxDto>
    {
        public GetCompanyTaxInfoQuery(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
    }
}