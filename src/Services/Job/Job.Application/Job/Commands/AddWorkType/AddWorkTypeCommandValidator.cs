using FluentValidation;

namespace Job.Application.Job.Commands.AddWorkType
{
    public class AddWorkTypeCommandValidator: AbstractValidator<AddWorkTypeCommand>
    {
        public AddWorkTypeCommandValidator()
        {
            RuleFor(x => x.JobId).NotNull().NotEmpty();
            RuleFor(x => x.WorkTypeId).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}