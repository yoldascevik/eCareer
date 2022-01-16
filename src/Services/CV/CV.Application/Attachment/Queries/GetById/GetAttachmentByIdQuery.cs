using Career.MediatR.Query;
using CurriculumVitae.Application.Attachment.Dtos;

namespace CurriculumVitae.Application.Attachment.Queries.GetById;

public class GetAttachmentByIdQuery : IQuery<AttachmentDto>
{
    public GetAttachmentByIdQuery(string attachmentId)
    {
        AttachmentId = attachmentId;
    }

    public string AttachmentId { get; }
}