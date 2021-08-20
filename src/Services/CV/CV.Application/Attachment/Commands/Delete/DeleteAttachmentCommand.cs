using Career.MediatR.Command;

namespace CurriculumVitae.Application.Attachment.Commands.Delete
{
    public class DeleteAttachmentCommand : ICommand
    {
        public DeleteAttachmentCommand(string attachmentId)
        {
            AttachmentId = attachmentId;
        }

        public string AttachmentId { get; }
    }
}