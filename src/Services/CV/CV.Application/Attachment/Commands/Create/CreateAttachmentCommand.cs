using Career.MediatR.Command;
using CurriculumVitae.Application.Attachment.Dtos;

namespace CurriculumVitae.Application.Attachment.Commands.Create;

public class CreateAttachmentCommand : AttachmentInputDto, ICommand<AttachmentDto>
{
}