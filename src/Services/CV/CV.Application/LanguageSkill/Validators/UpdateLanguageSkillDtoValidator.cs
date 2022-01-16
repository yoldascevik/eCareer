using CurriculumVitae.Application.LanguageSkill.Dtos;
using CurriculumVitae.Core.Constants;
using FluentValidation;

namespace CurriculumVitae.Application.LanguageSkill.Validators;

public class UpdateLanguageSkillDtoValidator : AbstractValidator<UpdateLanguageSkillDto>
{
    public UpdateLanguageSkillDtoValidator()
    {
        string[] availableLanguageLevels = LanguageSkillLevel.GetAll<LanguageSkillLevel>().Select(x => x.Id).ToArray();
            
        RuleFor(x => x.SkillLevel).NotEmpty()
            .Must(levelId => availableLanguageLevels.Contains(levelId))
            .WithMessage($"Language level not found! Available levels : {string.Join(",", availableLanguageLevels)}");
    }
}