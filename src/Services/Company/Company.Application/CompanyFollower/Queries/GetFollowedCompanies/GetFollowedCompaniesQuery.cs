using Career.Data.Pagination;
using Career.MediatR.Query;

namespace Company.Application.CompanyFollower.Queries.GetFollowedCompanies;

public class GetFollowedCompaniesQuery: IQuery<PagedList<Guid>>
{
    public GetFollowedCompaniesQuery(Guid userId, PaginationFilter paginationFilter)
    {
        UserId = userId;
        PaginationFilter = paginationFilter;
    }
        
    public Guid UserId { get; set; }
    public PaginationFilter PaginationFilter { get; set; }
}