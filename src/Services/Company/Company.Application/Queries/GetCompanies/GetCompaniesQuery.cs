using Career.Utilities.Pagination;
using MediatR;

namespace Company.Application.Queries.GetCompanies
{
    public class GetCompaniesQuery: PaginationFilter, IRequest<PagedList<CompanyDto>>
    {
        
    }
}