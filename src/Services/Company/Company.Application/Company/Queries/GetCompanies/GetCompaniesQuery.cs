using Career.Data.Pagination;
using Career.MediatR.Query;
using Company.Application.Company.Dtos;

namespace Company.Application.Company.Queries.GetCompanies;

public class GetCompaniesQuery: PaginationFilter, IQuery<PagedList<CompanyDto>>
{
        
}