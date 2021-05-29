using CurriculumVitae.Application.Disability.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.Disability.Commands.Update
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