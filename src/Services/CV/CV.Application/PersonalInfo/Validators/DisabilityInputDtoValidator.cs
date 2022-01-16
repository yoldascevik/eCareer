using CurriculumVitae.Application.PersonalInfo.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.PersonalInfo.Validators;

public class DisabilityInputDtoValidator : AbstractValidator<DisabilityInputDto>
{
    public DisabilityInputDtoValidator()
    {
        RuleFor(x => x.TypeId).NotEmpty();
        RuleFor(x => x.Rate).GreaterThan(0);
        RuleFor(x => x.Notes).MaximumLength(500);
    }
}