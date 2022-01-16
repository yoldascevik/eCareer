using System;
using Career.MediatR.Command;

namespace Job.Application.Job.Commands.RemoveWorkType;

public class RemoveWorkTypeCommand: ICommand
{
    public RemoveWorkTypeCommand(Guid jobId, string workTypeId)
    {
        JobId = jobId;
        WorkTypeId = workTypeId;
    }

    public Guid JobId { get; }
    public string WorkTypeId { get; }
}