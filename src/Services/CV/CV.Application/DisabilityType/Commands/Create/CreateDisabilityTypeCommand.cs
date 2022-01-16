using Career.MediatR.Command;

namespace CurriculumVitae.Application.DisabilityType.Commands.Create;

public class CreateDisabilityTypeCommand : ICommand<DisabilityTypeDto>
{
    public string Name { get; set; }
}