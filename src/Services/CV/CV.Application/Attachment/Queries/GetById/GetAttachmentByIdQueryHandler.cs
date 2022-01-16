using AutoMapper;
using Career.MediatR.Query;
using CurriculumVitae.Application.Attachment.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.Attachment.Queries.GetById;

public class GetAttachmentByIdQueryHandler : IQueryHandler<GetAttachmentByIdQuery, AttachmentDto>
{
    private readonly IMapper _mapper;
    private readonly IAttachmentRepository _attachmentRepository;

    public GetAttachmentByIdQueryHandler(IAttachmentRepository attachmentRepository, IMapper mapper)
    {
        _attachmentRepository = attachmentRepository;
        _mapper = mapper;
    }

    public async Task<AttachmentDto> Handle(GetAttachmentByIdQuery request, CancellationToken cancellationToken)
    {
        var attachment = await _attachmentRepository.GetByKeyAsync(request.AttachmentId);
        if (attachment == null || attachment.IsDeleted)
        {
            throw new AttachmentNotFoundException(request.AttachmentId);
        }

        return _mapper.Map<AttachmentDto>(attachment);
    }
}