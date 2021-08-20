using Career.MediatR.Command;

namespace CurriculumVitae.Application.DisabilityType.Commands.Update
{
    public class UpdateDisabilityTypeCommand : ICommand
    {
        public UpdateDisabilityTypeCommand(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}