using CurriculumVitae.Application.Cv.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.Cv.Commands.UpdateLocation
{
    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationCommandValidator()
        {
            RuleFor(x => x.CvId).NotNull();
            RuleFor(x => x.Location).SetValidator(new PersonLocationDtoValidator());
        }
    }
}