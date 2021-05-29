using Career.MediatR.Command;

namespace CurriculumVitae.Application.Disability.Commands.Delete
{
    public class DeleteDisabilityCommand : ICommand
    {
        public DeleteDisabilityCommand(string cvId, string id)
        {
            Id = id;
            CvId = cvId;
        }

        public string Id { get; }
        public string CvId { get; }
    }
}