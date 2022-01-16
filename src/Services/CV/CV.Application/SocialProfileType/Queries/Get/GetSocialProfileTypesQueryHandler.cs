using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.SocialProfileType.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.SocialProfileType.Queries.Get;

public class GetSocialProfileTypesQueryHandler : IQueryHandler<GetSocialProfileTypesQuery, PagedList<SocialProfileTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ISocialProfileTypeRepository _socialProfileTypeRepository;

    public GetSocialProfileTypesQueryHandler(ISocialProfileTypeRepository socialProfileTypeRepository, IMapper mapper)
    {
        _socialProfileTypeRepository = socialProfileTypeRepository;
        _mapper = mapper;
    }
        
    public async Task<PagedList<SocialProfileTypeDto>> Handle(GetSocialProfileTypesQuery request, CancellationToken cancellationToken)
    {
        return await _socialProfileTypeRepository.Get()
            .ExcludeDeletedItems()
            .OrderBy(x => x.Name)
            .ProjectTo<SocialProfileTypeDto>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}