using System;
using Career.MediatR.Command;

namespace Job.Application.Candidate.Commands.Withdraw;

public class WithdrawCandidateCommand: ICommand
{
    public WithdrawCandidateCommand(Guid candidateId)
    {
        CandidateId = candidateId;
    }

    public Guid CandidateId { get; }
}