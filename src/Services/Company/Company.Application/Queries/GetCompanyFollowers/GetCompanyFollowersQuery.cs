using System;
using Career.Data.Pagination;
using Company.Application.Dtos.Company;
using MediatR;

namespace Company.Application.Queries.GetCompanyFollowers
{
    public class GetCompanyFollowersQuery: PaginationFilter, IRequest<PagedList<CompanyFollowerDto>>
    {
        public GetCompanyFollowersQuery(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; set; }
    }
}