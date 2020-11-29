using Career.Data.Pagination;
using Career.MediatR.Query;
using Company.Application.Dtos.Company;
using MediatR;

namespace Company.Application.Queries.Company.GetCompanies
{
    public class GetCompaniesQuery: PaginationFilter, IQuery<PagedList<CompanyDto>>
    {
        
    }
}