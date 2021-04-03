using FluentValidation;

namespace Job.Application.Job.Commands.SendForApproval
{
    public class SendJobForApprovalCommandValidator : AbstractValidator<SendJobForApprovalCommand>
    {
        public SendJobForApprovalCommandValidator()
        {
            RuleFor(x => x.JobId).NotEmpty();
        }
    }
}