using ICommand = Career.MediatR.Command.ICommand;

namespace Job.Application.Job.Commands.Revoke;

public class RevokeJobCommand: ICommand
{
    public RevokeJobCommand(Guid jobId, string reason)
    {
        JobId = jobId;
        Reason = reason;
    }
        
    public Guid JobId { get; }
    public string Reason { get; }
}