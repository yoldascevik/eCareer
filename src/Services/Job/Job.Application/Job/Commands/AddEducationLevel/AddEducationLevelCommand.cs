using System;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Commands.AddEducationLevel
{
    public class AddEducationLevelCommand: ICommand<EducationLevelDto>
    {
        public AddEducationLevelCommand(Guid jobId, string educationLevelId, string name)
        {
            JobId = jobId;
            EducationLevelId = educationLevelId;
            Name = name;
        }

        public Guid JobId { get; }
        public string EducationLevelId { get;}
        public string Name { get;}
    }
}