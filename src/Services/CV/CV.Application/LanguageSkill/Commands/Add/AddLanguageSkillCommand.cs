using Career.MediatR.Command;
using CurriculumVitae.Application.LanguageSkill.Dtos;

namespace CurriculumVitae.Application.LanguageSkill.Commands.Add
{
    public class AddLanguageSkillCommand : ICommand<LanguageSkillDto>
    {
        public AddLanguageSkillCommand(string cvId, LanguageSkillInputDto languageSkill)
        {
            CvId = cvId;
            LanguageSkill = languageSkill;
        }

        public string CvId { get; }
        public LanguageSkillInputDto LanguageSkill { get; }
    }
}