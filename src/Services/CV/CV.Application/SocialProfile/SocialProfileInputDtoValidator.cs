using CurriculumVitae.Application.SocialProfile.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.SocialProfile;

public class SocialProfileInputDtoValidator : AbstractValidator<SocialProfileInputDto>
{
    public SocialProfileInputDtoValidator()
    {
        RuleFor(x => x.TypeId).NotEmpty();
        RuleFor(x => x.UserName).NotEmpty();
    }
}