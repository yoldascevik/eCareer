using Career.MediatR.Command;
using CurriculumVitae.Application.Attachment.Dtos;

namespace CurriculumVitae.Application.Attachment.Commands.Update
{
    public class UpdateAttachmentCommand: ICommand
    {
        public UpdateAttachmentCommand(string attachmentId, AttachmentInputDto attachment)
        {
            AttachmentId = attachmentId;
            Attachment = attachment;
        }

        public string AttachmentId { get; }
        public AttachmentInputDto Attachment { get; }
    }
}