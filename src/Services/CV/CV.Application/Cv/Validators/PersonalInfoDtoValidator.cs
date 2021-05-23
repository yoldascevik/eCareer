using CurriculumVitae.Application.Cv.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.Cv.Validators
{
    public class PersonalInfoDtoValidator : AbstractValidator<PersonalInfoDto>
    {
        public PersonalInfoDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(20);
            RuleFor(x => x.MiddleName).MaximumLength(20);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Gender).NotNull().IsInEnum();
            RuleFor(x => x.DateOfBirth).NotNull();
        }
    }
}