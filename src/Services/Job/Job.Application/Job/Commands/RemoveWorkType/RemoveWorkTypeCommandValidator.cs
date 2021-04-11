using FluentValidation;

namespace Job.Application.Job.Commands.RemoveWorkType
{
    public class RemoveWorkTypeCommandValidator: AbstractValidator<RemoveWorkTypeCommand>
    {
        public RemoveWorkTypeCommandValidator()
        {
            RuleFor(x => x.WorkTypeId).NotNull().NotEmpty();
            RuleFor(x => x.JobId).NotNull().NotEmpty();
        }
    }
}