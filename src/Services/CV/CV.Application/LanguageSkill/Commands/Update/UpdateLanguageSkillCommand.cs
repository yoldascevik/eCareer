using Career.MediatR.Command;
using CurriculumVitae.Application.LanguageSkill.Dtos;

namespace CurriculumVitae.Application.LanguageSkill.Commands.Update;

public class UpdateLanguageSkillCommand : ICommand
{
    public UpdateLanguageSkillCommand(string cvId, string languageSkillId, UpdateLanguageSkillDto languageSkill)
    {
        CvId = cvId;
        LanguageSkillId = languageSkillId;
        LanguageSkill = languageSkill;
    }

    public string CvId { get; }
    public string LanguageSkillId { get; }
    public UpdateLanguageSkillDto LanguageSkill { get; }
}