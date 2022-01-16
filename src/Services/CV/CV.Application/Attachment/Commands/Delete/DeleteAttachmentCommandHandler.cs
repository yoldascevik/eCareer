using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Attachment.Commands.Delete;

public class DeleteAttachmentCommandHandler : ICommandHandler<DeleteAttachmentCommand>
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly ILogger<DeleteAttachmentCommandHandler> _logger;

    public DeleteAttachmentCommandHandler(IAttachmentRepository attachmentRepository, ILogger<DeleteAttachmentCommandHandler> logger)
    {
        _attachmentRepository = attachmentRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachment = await _attachmentRepository.GetByKeyAsync(request.AttachmentId);
        if (attachment == null || attachment.IsDeleted)
        {
            throw new AttachmentNotFoundException(request.AttachmentId);
        }

        await _attachmentRepository.DeleteAsync(attachment.Id);
        _logger.LogInformation("Attachment \"{AttachmentId}\" is deleted", attachment.Id);
            
        return Unit.Value;
    }
}