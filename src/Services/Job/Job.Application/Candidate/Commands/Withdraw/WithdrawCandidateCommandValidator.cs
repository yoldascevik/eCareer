using FluentValidation;

namespace Job.Application.Candidate.Commands.Withdraw;

public class WithdrawCandidateCommandValidator: AbstractValidator<WithdrawCandidateCommand>
{
    public WithdrawCandidateCommandValidator()
    {
        RuleFor(x => x.CandidateId).NotNull().NotEmpty();
    }
}