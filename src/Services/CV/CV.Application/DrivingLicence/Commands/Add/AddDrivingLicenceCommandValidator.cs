using FluentValidation;

namespace CurriculumVitae.Application.DrivingLicence.Commands.Add
{
    public class AddDrivingLicenceCommandValidator : AbstractValidator<AddDrivingLicenceCommand>
    {
        public AddDrivingLicenceCommandValidator()
        {
            RuleFor(x => x.CvId).NotEmpty();
            RuleFor(x => x.DrivingLicence).SetValidator(new DrivingLicenceInputDtoValidator());
        }
    }
}