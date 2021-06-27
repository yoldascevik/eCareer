using FluentValidation;

namespace CurriculumVitae.Application.SocialProfile.Commands.Add
{
    public class AddSocialProfileCommandValidator : AbstractValidator<AddSocialProfileCommand>
    {
        public AddSocialProfileCommandValidator()
        {
            RuleFor(x => x.CvId).NotEmpty();
            RuleFor(x => x.SocialProfile).SetValidator(new SocialProfileInputDtoValidator());
        }
    }
 }