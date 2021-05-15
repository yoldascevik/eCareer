using System;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;

namespace Job.Application.Job.Commands.Create
{
    public class CreateJobCommand: ICommand<Guid>
    {
        public CompanyRefDto Company { get; set; }
        public JobInputDto Job { get; set; }
    }
}