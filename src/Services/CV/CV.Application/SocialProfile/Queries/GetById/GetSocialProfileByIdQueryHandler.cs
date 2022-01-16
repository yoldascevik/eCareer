using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.SocialProfile.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.SocialProfile.Queries.GetById;

public class GetSocialProfileByIdQueryHandler : IQueryHandler<GetSocialProfileByIdQuery, SocialProfileDto>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;

    public GetSocialProfileByIdQueryHandler(ICVRepository cvRepository, IMapper mapper)
    {
        _cvRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<SocialProfileDto> Handle(GetSocialProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
        {
            throw new CVNotFoundException(request.CvId);
        }

        var socialProfile = cv.SocialProfiles.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.SocialProfileId);
        if (socialProfile == null)
        {
            throw new SocialProfileNotFoundException(request.SocialProfileId);
        }
            
        return _mapper.Map<SocialProfileDto>(socialProfile);
    }
}