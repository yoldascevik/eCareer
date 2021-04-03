using System;
using Career.MediatR.Command;

namespace Job.Application.Job.Commands.RemoveLocation
{
    public class RemoveLocationCommand: ICommand
    {
        public RemoveLocationCommand(Guid jobId, Guid locationId)
        {
            JobId = jobId;
            LocationId = locationId;
        }

        public Guid JobId { get; }
        public Guid LocationId { get; }
    }
}