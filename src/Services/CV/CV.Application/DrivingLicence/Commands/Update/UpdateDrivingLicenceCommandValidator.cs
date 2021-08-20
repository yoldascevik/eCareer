using FluentValidation;

namespace CurriculumVitae.Application.DrivingLicence.Commands.Update
{
    public class UpdateDrivingLicenceCommandValidator : AbstractValidator<UpdateDrivingLicenceCommand>
    {
        public UpdateDrivingLicenceCommandValidator()
        {
            RuleFor(x => x.CvId).NotEmpty();
            RuleFor(x => x.DrivingLicenceId).NotEmpty();
            RuleFor(x => x.DrivingLicence).SetValidator(new DrivingLicenceInputDtoValidator());
        }
    }
}