using CurriculumVitae.Application.Cv.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.Cv.Commands.Create
{
    public class CreateCVCommandValidator: AbstractValidator<CreateCVCommand>
    {
        public CreateCVCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.PersonalInfo).SetValidator(new PersonalInfoDtoValidator());
        }
    }
}