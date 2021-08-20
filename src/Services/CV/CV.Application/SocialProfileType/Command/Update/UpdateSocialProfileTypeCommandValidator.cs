using CurriculumVitae.Application.SocialProfileType.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.SocialProfileType.Command.Update
{
    public class UpdateSocialProfileTypeCommandValidator : AbstractValidator<UpdateSocialProfileTypeCommand>
    {
        public UpdateSocialProfileTypeCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.SocialProfileType).SetValidator(new SocialProfileInputDtoValidator());
        }
    }
}