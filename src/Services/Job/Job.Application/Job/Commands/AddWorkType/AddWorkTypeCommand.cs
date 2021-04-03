using System;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Commands.AddWorkType
{
    public class AddWorkTypeCommand: ICommand<WorkTypeDto>
    {
        public AddWorkTypeCommand(Guid jobId, string workTypeId, string name)
        {
            JobId = jobId;
            WorkTypeId = workTypeId;
            Name = name;
        }

        public Guid JobId { get; }
        public string WorkTypeId { get;}
        public string Name { get;}
    }
}