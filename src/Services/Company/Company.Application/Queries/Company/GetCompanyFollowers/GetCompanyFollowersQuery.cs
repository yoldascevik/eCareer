using System;
using Career.Data.Pagination;
using MediatR;

namespace Company.Application.Queries.Company.GetCompanyFollowers
{
    public class GetCompanyFollowersQuery: IRequest<PagedList<Guid>>
    {
        public GetCompanyFollowersQuery(Guid companyId, PaginationFilter paginationFilter)
        {
            CompanyId = companyId;
            PaginationFilter = paginationFilter;
        }
        
        public Guid CompanyId { get; set; }
        public PaginationFilter PaginationFilter { get; }
    }
}