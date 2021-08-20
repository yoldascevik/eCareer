using CurriculumVitae.Application.PersonalInfo.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.PersonalInfo.Commands.UpdateDisability
{
    public class UpdateDisabilityCommandValidator : AbstractValidator<UpdateDisabilityCommand>
    {
        public UpdateDisabilityCommandValidator()
        {
            RuleFor(x => x.CvId).NotEmpty();
            RuleFor(x => x.DisabilityId).NotEmpty();
            RuleFor(x => x.DisabilityInfo).SetValidator(new DisabilityInputDtoValidator());
        }
    }
}