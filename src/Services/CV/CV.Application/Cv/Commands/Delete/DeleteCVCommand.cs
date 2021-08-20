using Career.Exceptions;
using Career.MediatR.Command;

namespace CurriculumVitae.Application.Cv.Commands.Delete
{
    public class DeleteCVCommand: ICommand
    {
        public DeleteCVCommand(string id)
        {
            Check.NotNullOrEmpty(id, nameof(id));
            Id = id;
        }
        
        public string Id { get; }
    }
}