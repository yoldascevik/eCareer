using System;
using Career.MediatR.Command;

namespace Job.Application.Job.Commands.Delete;

public class DeleteJobCommand: ICommand
{
    public DeleteJobCommand(Guid jobId)
    {
        JobId = jobId;
    }
        
    public Guid JobId { get; }
}