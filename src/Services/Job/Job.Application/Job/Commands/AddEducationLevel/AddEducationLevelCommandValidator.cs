using FluentValidation;
using Job.Application.Job.Validators;

namespace Job.Application.Job.Commands.AddEducationLevel
{
    public class AddEducationLevelCommandValidator: AbstractValidator<AddEducationLevelCommand>
    {
        public AddEducationLevelCommandValidator()
        {
            RuleFor(x => x.JobId).NotNull().NotEmpty();
            RuleFor(x => x.EducationLevelDto).SetValidator(new IdNameRefDtoValidator());
        }
    }
}