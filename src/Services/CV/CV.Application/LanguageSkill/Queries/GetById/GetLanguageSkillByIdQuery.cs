using Career.MediatR.Query;
using CurriculumVitae.Application.LanguageSkill.Dtos;

namespace CurriculumVitae.Application.LanguageSkill.Queries.GetById;

public class GetLanguageSkillByIdQuery : IQuery<LanguageSkillDto>
{
    public GetLanguageSkillByIdQuery(string cvId, string languageSkillId)
    {
        CvId = cvId;
        LanguageSkillId = languageSkillId;
    }

    public string CvId { get; }
    public string LanguageSkillId { get; }
}