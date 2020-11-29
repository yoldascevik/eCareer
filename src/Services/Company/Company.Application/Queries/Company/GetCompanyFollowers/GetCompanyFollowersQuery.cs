using System;
using Career.Data.Pagination;
using Career.MediatR.Query;
using MediatR;

namespace Company.Application.Queries.Company.GetCompanyFollowers
{
    public class GetCompanyFollowersQuery: IQuery<PagedList<Guid>>
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