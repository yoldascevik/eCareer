using System;
using Career.Exceptions;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Commands.Update
{
    public class UpdateJobCommand: ICommand<JobDetailDto>
    {
        public UpdateJobCommand(Guid jobId, JobInputDto job)
        {
            Check.NotNull(job, nameof(job));
            
            Job = job;
            JobId = jobId;
        }
        
        public Guid JobId { get; }
        public JobInputDto Job { get; }
    }
}