using CurriculumVitae.Application.Disability.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.Disability.Commands.Add
{
    public class AddDisabilityCommandValidator : AbstractValidator<AddDisabilityCommand>
    {
        public AddDisabilityCommandValidator()
        {
            RuleFor(x => x.CvId).NotEmpty();
            RuleFor(x => x.DisabilityInfo).SetValidator(new DisabilityInputDtoValidator());
        }
    }
}