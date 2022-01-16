using Career.MediatR.Command;

namespace CurriculumVitae.Application.DisabilityType.Commands.Delete;

public class DeleteDisabilityTypeCommand : ICommand
{
    public DeleteDisabilityTypeCommand(string id)
    {
        Id = id;
    }
        
    public string Id { get; }
}