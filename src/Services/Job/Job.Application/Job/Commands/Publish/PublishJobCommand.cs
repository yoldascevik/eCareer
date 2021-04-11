using System;
using ICommand = Career.MediatR.Command.ICommand;

namespace Job.Application.Job.Commands.Publish
{
    public class PublishJobCommand: ICommand
    {
        public PublishJobCommand(Guid jobId, DateTime validityDate)
        {
            JobId = jobId;
            ValidityDate = validityDate;
        }
        
        public Guid JobId { get; }
        public DateTime ValidityDate { get; }
    }
}