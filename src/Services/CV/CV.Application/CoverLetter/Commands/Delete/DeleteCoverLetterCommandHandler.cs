using Career.MediatR.Command;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.CoverLetter.Commands.Delete;

public class DeleteCoverLetterCommandHandler : ICommandHandler<DeleteCoverLetterCommand>
{
    private readonly ICoverLetterRepository _coverLetterRepository;
    private readonly ILogger<DeleteCoverLetterCommandHandler> _logger;

    public DeleteCoverLetterCommandHandler(ICoverLetterRepository coverLetterRepository, ILogger<DeleteCoverLetterCommandHandler> logger)
    {
        _coverLetterRepository = coverLetterRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteCoverLetterCommand request, CancellationToken cancellationToken)
    {
        var coverLetter = await _coverLetterRepository.GetByKeyAsync(request.CoverLetterId);
        if (coverLetter == null || coverLetter.IsDeleted)
        {
            throw new CoverLetterNotFoundException(request.CoverLetterId);
        }

        await _coverLetterRepository.DeleteAsync(coverLetter.Id);
        _logger.LogInformation("Cover letter \"{CoverLetterId}\" is deleted", coverLetter.Id);
            
        return Unit.Value;
    }
}