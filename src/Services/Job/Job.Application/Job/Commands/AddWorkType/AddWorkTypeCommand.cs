using System;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Commands.AddWorkType;

public class AddWorkTypeCommand: ICommand<IdNameRefDto>
{
    public AddWorkTypeCommand(Guid jobId, IdNameRefDto workTypeDto)
    {
        JobId = jobId;
        WorkTypeDto = workTypeDto;
    }

    public Guid JobId { get; }
    public IdNameRefDto WorkTypeDto { get; }
}