using Career.MediatR.Command;

namespace Job.Application.Tag.Commands.Delete;

public class DeleteTagCommand: ICommand
{
    public DeleteTagCommand(Guid tagId)
    {
        TagId = tagId;
    }
        
    public Guid TagId { get; }
}