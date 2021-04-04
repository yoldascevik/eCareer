using System;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Commands.AddWorkType
{
    public class AddWorkTypeCommand: ICommand<WorkTypeDto>
    {
        public AddWorkTypeCommand(Guid jobId, WorkTypeDto workTypeDto)
        {
            JobId = jobId;
            WorkTypeDto = workTypeDto;
        }

        public Guid JobId { get; }
        public WorkTypeDto WorkTypeDto { get; }
    }
}