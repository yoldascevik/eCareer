using Career.Data.Pagination;
using Career.Exceptions;
using Career.MediatR.Query;
using Company.Application.Company.Dtos;

namespace Company.Application.Company.Queries.GetCompanyAddresses;

public class GetCompanyAddressesQuery: IQuery<PagedList<AddressDto>>
{
    public GetCompanyAddressesQuery(Guid companyId, PaginationFilter paginationFilter)
    {
        Check.NotEmpty(companyId, nameof(companyId));
        Check.NotNull(paginationFilter, nameof(paginationFilter));
            
        CompanyId = companyId;
        PaginationFilter = paginationFilter;
    }

    public Guid CompanyId { get; }
    public PaginationFilter PaginationFilter { get;}
}