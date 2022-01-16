using Career.Data.Pagination;
using Career.MediatR.Query;
using Job.Application.Tag.Dtos;

namespace Job.Application.Tag.Queries.Get;

public class GetTagsQuery:  PaginationFilter, IQuery<PagedList<TagDto>>
{
}