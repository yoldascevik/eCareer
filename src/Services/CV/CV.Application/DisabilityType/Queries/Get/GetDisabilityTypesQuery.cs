using Career.Data.Pagination;
using Career.MediatR.Query;

namespace CurriculumVitae.Application.DisabilityType.Queries.Get;

public class GetDisabilityTypesQuery : PaginationFilter, IQuery<PagedList<DisabilityTypeDto>>
{
}