using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.LanguageSkill.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.LanguageSkill.Queries.Get;

public class GetLanguageSkillsQueryHandler : IQueryHandler<GetLanguageSkillsQuery, List<LanguageSkillDto>>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;

    public GetLanguageSkillsQueryHandler(ICVRepository cvRepository, IMapper mapper)
    {
        _cvRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<List<LanguageSkillDto>> Handle(GetLanguageSkillsQuery request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
        {
            throw new CVNotFoundException(request.CvId);
        }

        return _mapper.Map<List<LanguageSkillDto>>(cv.LanguageSkills);
    }
}