using Career.Data.Pagination;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv.Dtos;

namespace CurriculumVitae.Application.Cv.Queries.Get;

public class GetCVsQuery: PaginationFilter, IQuery<PagedList<CVSummaryDto>>
{
        
}