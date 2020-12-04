using Career.Data.Pagination;
using Career.MediatR.Query;
using Company.Application.Company.Models;

namespace Company.Application.Company.Queries.GetCompanies
{
    public class GetCompaniesQuery: PaginationFilter, IQuery<PagedList<CompanyDto>>
    {
        
    }
}