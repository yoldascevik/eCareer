using CurriculumVitae.Application.LanguageSkill.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.LanguageSkill.Commands.Update;

public class UpdateLanguageSkillCommandValidator : AbstractValidator<UpdateLanguageSkillCommand>
{
    public UpdateLanguageSkillCommandValidator()
    {
        RuleFor(x => x.CvId).NotEmpty();
        RuleFor(x => x.LanguageSkillId).NotEmpty();
        RuleFor(x => x.LanguageSkill).SetValidator(new UpdateLanguageSkillDtoValidator());
    }
}