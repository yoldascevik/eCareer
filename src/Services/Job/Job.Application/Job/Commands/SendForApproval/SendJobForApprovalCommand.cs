using System;
using ICommand = Career.MediatR.Command.ICommand;

namespace Job.Application.Job.Commands.SendForApproval;

public class SendJobForApprovalCommand: ICommand
{
    public SendJobForApprovalCommand(Guid jobId)
    {
        JobId = jobId;
    }
        
    public Guid JobId { get; }
}