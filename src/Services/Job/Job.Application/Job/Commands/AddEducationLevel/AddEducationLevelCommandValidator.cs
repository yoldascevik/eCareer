using FluentValidation;

namespace Job.Application.Job.Commands.AddEducationLevel
{
    public class AddEducationLevelCommandValidator: AbstractValidator<AddEducationLevelCommand>
    {
        public AddEducationLevelCommandValidator()
        {
            RuleFor(x => x.JobId).NotNull().NotEmpty();
            RuleFor(x => x.EducationLevelId).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}