using System;
using Career.MediatR.Command;

namespace Job.Application.Job.Commands.DeleteJob
{
    public class DeleteJobCommand: ICommand
    {
        public DeleteJobCommand(Guid jobId)
        {
            JobId = jobId;
        }
        
        public Guid JobId { get; }
    }
}