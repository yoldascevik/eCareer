using FluentValidation;

namespace Job.Application.Job.Commands.RemoveEducationLevel;

public class RemoveEducationLevelCommandValidator: AbstractValidator<RemoveEducationLevelCommand>
{
    public RemoveEducationLevelCommandValidator()
    {
        RuleFor(x => x.EducationLevelId).NotNull().NotEmpty();
        RuleFor(x => x.JobId).NotNull().NotEmpty();
    }
}