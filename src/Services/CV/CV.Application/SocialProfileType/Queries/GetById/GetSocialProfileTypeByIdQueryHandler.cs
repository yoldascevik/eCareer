using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Query;
using CurriculumVitae.Application.SocialProfileType.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.SocialProfileType.Queries.GetById;

public class GetSocialProfileTypeByIdQueryHandler : IQueryHandler<GetSocialProfileTypeByIdQuery, SocialProfileTypeDto>
{
    private readonly IMapper _mapper;
    private readonly ISocialProfileTypeRepository _socialProfileTypeRepository;

    public GetSocialProfileTypeByIdQueryHandler(ISocialProfileTypeRepository socialProfileTypeRepository, IMapper mapper)
    {
        _socialProfileTypeRepository = socialProfileTypeRepository;
        _mapper = mapper;
    }

    public async Task<SocialProfileTypeDto> Handle(GetSocialProfileTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var socialProfileType = await _socialProfileTypeRepository.GetByKeyAsync(request.Id);
        if (socialProfileType == null || socialProfileType.IsDeleted)
            throw new SocialProfileTypeNotFoundException(request.Id);

        return _mapper.Map<SocialProfileTypeDto>(socialProfileType);
    }
}