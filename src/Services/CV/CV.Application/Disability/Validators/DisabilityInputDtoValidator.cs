using CurriculumVitae.Application.Disability.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.Disability.Validators
{
    public class DisabilityInputDtoValidator : AbstractValidator<DisabilityInputDto>
    {
        public DisabilityInputDtoValidator()
        {
            RuleFor(x => x.TypeId).NotEmpty();
            RuleFor(x => x.Rate).GreaterThan(0);
            RuleFor(x => x.Notes).MaximumLength(500);
        }
    }
}