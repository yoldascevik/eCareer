using Career.Data.Pagination;
using Company.Application.Dtos.Company;
using MediatR;

namespace Company.Application.Queries.GetCompanies
{
    public class GetCompaniesQuery: PaginationFilter, IRequest<PagedList<CompanyDto>>
    {
        
    }
}