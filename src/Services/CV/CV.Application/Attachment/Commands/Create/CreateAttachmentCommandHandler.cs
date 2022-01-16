using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Career.Shared.Timing;
using CurriculumVitae.Application.Attachment.Dtos;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Attachment.Commands.Create;

public class CreateAttachmentCommandHandler : ICommandHandler<CreateAttachmentCommand, AttachmentDto>
{
    private readonly IMapper _mapper;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly ILogger<CreateAttachmentCommandHandler> _logger;

    public CreateAttachmentCommandHandler(IAttachmentRepository attachmentRepository, IMapper mapper, ILogger<CreateAttachmentCommandHandler> logger)
    {
        _attachmentRepository = attachmentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AttachmentDto> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachment = await _attachmentRepository.AddAsync(_mapper.Map<Core.Entities.Attachment>(request));
            
        attachment.UploadedUserId = Guid.Empty; // TODO
        attachment.UploadDate = Clock.Now;

        char extensionChar = '.';
        if (attachment.FileExtension.StartsWith(extensionChar))
        {
            attachment.FileExtension = attachment.FileExtension.TrimStart(extensionChar);
        }
            
        _logger.LogInformation("Attachment \"{Description} ({AttachmentId})\" created", attachment.Description, attachment.Id);
            
        return _mapper.Map<AttachmentDto>(attachment);
    }
}