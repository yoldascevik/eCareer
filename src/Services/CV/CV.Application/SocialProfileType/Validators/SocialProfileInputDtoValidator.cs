using CurriculumVitae.Application.SocialProfileType.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.SocialProfileType.Validators
{
    public class SocialProfileInputDtoValidator : AbstractValidator<SocialProfileTypeInputDto>
    {
        public SocialProfileInputDtoValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.ProfileUrlPrefix).NotNull().SetValidator(new SocialProfileUrlPrefixValidator());
        }
    }
}