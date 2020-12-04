using System;
using Career.MediatR.Query;
using Company.Application.Company.Models;

namespace Company.Application.Company.Queries.GetCompanyById
{
    public class GetCompanyByIdQuery: IQuery<CompanyDto>
    {
        public GetCompanyByIdQuery(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
    }
}