using Career.Data.Pagination;
using Career.MediatR.Query;

namespace Company.Application.Company.Queries.GetCompanies
{
    public class GetCompaniesQuery: PaginationFilter, IQuery<PagedList<CompanyDto>>
    {
        
    }
}