using CurriculumVitae.Application.PersonalInfo.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.PersonalInfo.Commands.AddDisability
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