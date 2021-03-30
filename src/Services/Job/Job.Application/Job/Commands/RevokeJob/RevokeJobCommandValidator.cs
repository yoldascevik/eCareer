using FluentValidation;

namespace Job.Application.Job.Commands.RevokeJob
{
    public class RevokeJobCommandValidator : AbstractValidator<RevokeJobCommand>
    {
        public RevokeJobCommandValidator()
        {
            RuleFor(x => x.JobId).NotEmpty();
            RuleFor(x => x.Reason).NotEmpty().MinimumLength(5);
        }
    }
}