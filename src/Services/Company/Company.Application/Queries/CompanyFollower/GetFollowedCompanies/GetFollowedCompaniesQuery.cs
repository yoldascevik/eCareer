using System;
using Career.Data.Pagination;
using Company.Application.Dtos.Company;
using MediatR;

namespace Company.Application.Queries.CompanyFollower.GetFollowedCompanies
{
    public class GetFollowedCompaniesQuery: IRequest<PagedList<CompanyFollowerDto>>
    {
        public GetFollowedCompaniesQuery(Guid userId, PaginationFilter paginationFilter)
        {
            UserId = userId;
            PaginationFilter = paginationFilter;
        }
        
        public Guid UserId { get; set; }
        public PaginationFilter PaginationFilter { get; set; }
    }
}