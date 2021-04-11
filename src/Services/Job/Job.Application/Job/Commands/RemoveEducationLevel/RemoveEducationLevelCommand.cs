using System;
using Career.MediatR.Command;

namespace Job.Application.Job.Commands.RemoveEducationLevel
{
    public class RemoveEducationLevelCommand: ICommand
    {
        public RemoveEducationLevelCommand(Guid jobId, string educationLevelId)
        {
            JobId = jobId;
            EducationLevelId = educationLevelId;
        }

        public Guid JobId { get; }
        public string EducationLevelId { get; }
    }
}