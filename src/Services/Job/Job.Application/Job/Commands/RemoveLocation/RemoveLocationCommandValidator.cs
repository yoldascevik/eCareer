using FluentValidation;

namespace Job.Application.Job.Commands.RemoveLocation
{
    public class RemoveLocationCommandValidator: AbstractValidator<RemoveLocationCommand>
    {
        public RemoveLocationCommandValidator()
        {
            RuleFor(x => x.LocationId).NotNull().NotEmpty();
            RuleFor(x => x.JobId).NotNull().NotEmpty();
        }
    }
}