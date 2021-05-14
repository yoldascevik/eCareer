using FluentValidation;
using Job.Application.Job.Validators;

namespace Job.Application.Job.Commands.AddWorkType
{
    public class AddWorkTypeCommandValidator: AbstractValidator<AddWorkTypeCommand>
    {
        public AddWorkTypeCommandValidator()
        {
            RuleFor(x => x.JobId).NotNull().NotEmpty();
            RuleFor(x => x.WorkTypeDto).SetValidator(new IdNameRefDtoValidator());
        }
    }
}