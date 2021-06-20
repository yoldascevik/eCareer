using CurriculumVitae.Application.PersonalInfo.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.PersonalInfo.Validators
{
    public class PersonalInfoInputDtoValidator : AbstractValidator<PersonalInfoInputDto>
    {
        public PersonalInfoInputDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(20);
            RuleFor(x => x.MiddleName).MaximumLength(20);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Gender).NotNull().IsInEnum();
            RuleFor(x => x.DateOfBirth).NotNull();
        }
    }
}