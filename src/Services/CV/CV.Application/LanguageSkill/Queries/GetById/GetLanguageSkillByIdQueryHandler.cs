using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.LanguageSkill.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.LanguageSkill.Queries.GetById;

public class GetLanguageSkillByIdQueryHandler : IQueryHandler<GetLanguageSkillByIdQuery, LanguageSkillDto>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;

    public GetLanguageSkillByIdQueryHandler(ICVRepository cvRepository, IMapper mapper)
    {
        _cvRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<LanguageSkillDto> Handle(GetLanguageSkillByIdQuery request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
        {
            throw new CVNotFoundException(request.CvId);
        }

        var languageSkill = cv.LanguageSkills.FirstOrDefault(x => x.Id == request.LanguageSkillId);
        if (languageSkill == null)
        {
            throw new LanguageSkillNotFoundException(request.LanguageSkillId);
        }
            
        return _mapper.Map<LanguageSkillDto>(languageSkill);
    }
}