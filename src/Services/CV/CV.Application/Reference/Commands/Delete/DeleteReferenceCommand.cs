using Career.MediatR.Command;

namespace CurriculumVitae.Application.Reference.Commands.Delete
{
    public class DeleteReferenceCommand : ICommand
    {
        public DeleteReferenceCommand(string cvId, string referenceId)
        {
            CvId = cvId;
            ReferenceId = referenceId;
        }

        public string CvId { get; }
        public string ReferenceId { get; }
    }
}