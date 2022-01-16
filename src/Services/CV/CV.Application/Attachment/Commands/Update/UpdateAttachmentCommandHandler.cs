using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Career.Shared.Timing;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Attachment.Commands.Update;

public class UpdateAttachmentCommandHandler : ICommandHandler<UpdateAttachmentCommand>
{
    private readonly IMapper _mapper;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly ILogger<UpdateAttachmentCommandHandler> _logger;

    public UpdateAttachmentCommandHandler(IAttachmentRepository attachmentRepository, IMapper mapper, ILogger<UpdateAttachmentCommandHandler> logger)
    {
        _attachmentRepository = attachmentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachment = await _attachmentRepository.GetByKeyAsync(request.AttachmentId);
        if (attachment == null || attachment.IsDeleted)
        {
            throw new AttachmentNotFoundException(request.AttachmentId);
        }
            
        _mapper.Map(request.Attachment, attachment);
            
        attachment.UploadedUserId = Guid.Empty; // TODO
        attachment.UploadDate = Clock.Now;

        char extensionChar = '.';
        if (attachment.FileExtension.StartsWith(extensionChar))
        {
            attachment.FileExtension = attachment.FileExtension.TrimStart(extensionChar);
        }
            
        await _attachmentRepository.UpdateAsync(attachment.Id, attachment);

        _logger.LogInformation("Attachment \"{AttachmentId}\" is updated", attachment.Id);
            
        return Unit.Value;
    }
}