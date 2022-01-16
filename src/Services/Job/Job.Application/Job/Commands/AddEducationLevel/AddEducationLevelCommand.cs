using System;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Commands.AddEducationLevel;

public class AddEducationLevelCommand: ICommand<IdNameRefDto>
{
    public AddEducationLevelCommand(Guid jobId, IdNameRefDto educationLevelDto)
    {
        JobId = jobId;
        EducationLevelDto = educationLevelDto;
    }

    public Guid JobId { get; }
    public IdNameRefDto EducationLevelDto { get; }
}