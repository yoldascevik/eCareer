using System;
using Company.Application.Dtos.Company;
using MediatR;

namespace Company.Application.Queries.Company.GetCompanyById
{
    public class GetCompanyByIdQuery: IRequest<CompanyDto>
    {
        public GetCompanyByIdQuery(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
    }
}