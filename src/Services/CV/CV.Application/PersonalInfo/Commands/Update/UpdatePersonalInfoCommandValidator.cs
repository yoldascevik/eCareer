using CurriculumVitae.Application.PersonalInfo.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.PersonalInfo.Commands.Update;

public class UpdatePersonalInfoCommandValidator : AbstractValidator<UpdatePersonalInfoCommand>
{
    public UpdatePersonalInfoCommandValidator()
    {
        RuleFor(x => x.CvId).NotEmpty();
        RuleFor(x => x.PersonalInfo).SetValidator(new PersonalInfoInputDtoValidator());
    }
}