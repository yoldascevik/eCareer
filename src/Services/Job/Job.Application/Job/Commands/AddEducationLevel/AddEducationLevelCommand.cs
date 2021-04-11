using System;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Commands.AddEducationLevel
{
    public class AddEducationLevelCommand: ICommand<EducationLevelDto>
    {
        public AddEducationLevelCommand(Guid jobId, EducationLevelDto educationLevelDto)
        {
            JobId = jobId;
            EducationLevelDto = educationLevelDto;
        }

        public Guid JobId { get; }
        public EducationLevelDto EducationLevelDto { get; }
    }
}