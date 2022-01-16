using CurriculumVitae.Application.SocialProfileType.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.SocialProfileType.Command.Create;

public class CreateSocialProfileTypeCommandValidator : AbstractValidator<CreateSocialProfileTypeCommand>
{
    public CreateSocialProfileTypeCommandValidator()
    {
        RuleFor(x => x).SetValidator(new SocialProfileInputDtoValidator());
    }
}