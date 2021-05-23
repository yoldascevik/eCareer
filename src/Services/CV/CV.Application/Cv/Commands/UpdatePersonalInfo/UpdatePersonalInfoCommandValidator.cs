using CurriculumVitae.Application.Cv.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.Cv.Commands.UpdatePersonalInfo
{
    public class UpdatePersonalInfoCommandValidator : AbstractValidator<UpdatePersonalInfoCommand>
    {
        public UpdatePersonalInfoCommandValidator()
        {
            RuleFor(x => x.CvId).NotEmpty();
            RuleFor(x => x.PersonalInfo).SetValidator(new PersonalInfoDtoValidator());
        }
    }
}