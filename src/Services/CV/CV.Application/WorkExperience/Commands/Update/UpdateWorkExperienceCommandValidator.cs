using FluentValidation;

namespace CurriculumVitae.Application.WorkExperience.Commands.Update;

public class UpdateWorkExperienceCommandValidator : AbstractValidator<UpdateWorkExperienceCommand>
{
    public UpdateWorkExperienceCommandValidator()
    {
        RuleFor(x => x.CvId).NotEmpty();
        RuleFor(x => x.WorkExperienceId).NotEmpty();
        RuleFor(x => x.WorkExperience).SetValidator(new WorkExperienceInputDtoValidator());
    }
}