using System.Collections.Generic;
using Career.MediatR.Query;
using CurriculumVitae.Application.LanguageSkill.Dtos;

namespace CurriculumVitae.Application.LanguageSkill.Queries.Get;

public class GetLanguageSkillsQuery : IQuery<List<LanguageSkillDto>>
{
    public GetLanguageSkillsQuery(string cvId)
    {
        CvId = cvId;
    }

    public string CvId { get; }
}