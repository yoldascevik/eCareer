using System.Data;
using FluentValidation;

namespace CurriculumVitae.Application.SocialProfile.Commands.Update
{
    public class UpdateSocialProfileCommandValidator : AbstractValidator<UpdateSocialProfileCommand>
    {
        public UpdateSocialProfileCommandValidator()
        {
            RuleFor(x => x.CvId).NotEmpty();
            RuleFor(x => x.SocialProfileId).NotEmpty();
            RuleFor(x => x.SocialProfile).SetValidator(new SocialProfileInputDtoValidator());
        }
    }
}