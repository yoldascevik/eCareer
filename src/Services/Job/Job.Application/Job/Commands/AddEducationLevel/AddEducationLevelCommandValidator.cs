using FluentValidation;

namespace Job.Application.Job.Commands.AddEducationLevel
{
    public class AddEducationLevelCommandValidator: AbstractValidator<AddEducationLevelCommand>
    {
        public AddEducationLevelCommandValidator()
        {
            RuleFor(x => x.JobId).NotNull().NotEmpty();
            RuleFor(x => x.EducationLevelDto.Id).NotNull().NotEmpty();
            RuleFor(x => x.EducationLevelDto.Name).NotNull().NotEmpty();
        }
    }
}