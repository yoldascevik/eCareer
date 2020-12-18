using System;
using Career.MediatR.Query;
using Company.Application.Company.Dtos;

namespace Company.Application.Company.Queries.GetCompanyDetails
{
    public class GetCompanyDetailsQuery: IQuery<CompanyDetailDto>
    {
        public GetCompanyDetailsQuery(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
    }
}