using FluentValidation;
using Job.Application.Job.Commands.SendForApproval;

namespace Job.Application.Job.Commands.SendJobForApproval
{
    public class SendJobForApprovalCommandValidator : AbstractValidator<SendJobForApprovalCommand>
    {
        public SendJobForApprovalCommandValidator()
        {
            RuleFor(x => x.JobId).NotEmpty();
        }
    }
}