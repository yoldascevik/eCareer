using Career.Data.Pagination;
using Career.MediatR.Query;
using CurriculumVitae.Application.SocialProfileType.Dtos;

namespace CurriculumVitae.Application.SocialProfileType.Queries.Get
{
    public class GetSocialProfileTypesQuery : PaginationFilter, IQuery<PagedList<SocialProfileTypeDto>>
    {
        
    }
}