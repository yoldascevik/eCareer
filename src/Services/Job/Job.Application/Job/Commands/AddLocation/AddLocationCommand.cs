using System;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Commands.AddLocation
{
    public class AddLocationCommand: ICommand<JobLocationDto>
    {
        public AddLocationCommand(Guid jobId, JobLocationInputDto locationInputDto)
        {
            JobId = jobId;
            LocationInputDto = locationInputDto;
        }

        public Guid JobId { get; }
        public JobLocationInputDto LocationInputDto { get; }
    }
}