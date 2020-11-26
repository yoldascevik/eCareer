using System;
using Career.Data.Pagination;
using MediatR;

namespace Company.Application.Queries.CompanyFollower.GetFollowedCompanies
{
    public class GetFollowedCompaniesQuery: IRequest<PagedList<Guid>>
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