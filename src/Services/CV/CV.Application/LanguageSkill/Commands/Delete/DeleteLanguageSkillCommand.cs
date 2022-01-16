using Career.MediatR.Command;

namespace CurriculumVitae.Application.LanguageSkill.Commands.Delete;

public class DeleteLanguageSkillCommand : ICommand
{
    public DeleteLanguageSkillCommand(string cvId, string languageSkillId)
    {
        CvId = cvId;
        LanguageSkillId = languageSkillId;
    }

    public string CvId { get; }
    public string LanguageSkillId { get; }
}