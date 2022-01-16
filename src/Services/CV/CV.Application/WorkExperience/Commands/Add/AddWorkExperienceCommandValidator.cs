using FluentValidation;

namespace CurriculumVitae.Application.WorkExperience.Commands.Add;

public class AddWorkExperienceCommandValidator : AbstractValidator<AddWorkExperienceCommand>
{
    public AddWorkExperienceCommandValidator()
    {
        RuleFor(x => x.CvId).NotEmpty();
        RuleFor(x => x.WorkExperience).SetValidator(new WorkExperienceInputDtoValidator());
    }
}