using System;
using Career.Data.Pagination;
using Company.Application.Dtos.Company;
using MediatR;

namespace Company.Application.Queries.Company.GetCompanyFollowers
{
    public class GetCompanyFollowersQuery: PaginationFilter, IRequest<PagedList<CompanyFollowerDto>>
    {
        public Guid CompanyId { get; set; }
    }
}