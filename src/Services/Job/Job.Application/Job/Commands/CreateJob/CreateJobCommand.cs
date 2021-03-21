using System;
using Career.Exceptions;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Commands.CreateJob
{
    public class CreateJobCommand: ICommand<Guid>
    {
        public CreateJobCommand(Guid companyId, JobInputDto job)
        {
            Check.NotNull(job, nameof(job));
            
            Job = job;
            CompanyId = companyId;
        }
        
        public Guid CompanyId { get; }
        public JobInputDto Job { get; }
    }
}