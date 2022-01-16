using CurriculumVitae.Application.LanguageSkill.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.LanguageSkill.Commands.Add;

public class AddLanguageSkillCommandValidator : AbstractValidator<AddLanguageSkillCommand>
{
    public AddLanguageSkillCommandValidator()
    {
        RuleFor(x => x.CvId).NotEmpty();
        RuleFor(x => x.LanguageSkill).SetValidator(new LanguageSkillInputDtoValidator());
    }
}