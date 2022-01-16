using Career.Data.Pagination;
using Career.MediatR.Query;

namespace Company.Application.Company.Queries.GetCompanyFollowers;

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