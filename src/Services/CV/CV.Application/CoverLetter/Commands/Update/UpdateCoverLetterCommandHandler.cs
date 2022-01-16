using AutoMapper;
using Career.MediatR.Command;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.CoverLetter.Commands.Update;

public class UpdateCoverLetterCommandHandler : ICommandHandler<UpdateCoverLetterCommand>
{
    private readonly IMapper _mapper;
    private readonly ICoverLetterRepository _coverLetterRepository;
    private readonly ILogger<UpdateCoverLetterCommandHandler> _logger;

    public UpdateCoverLetterCommandHandler(ICoverLetterRepository coverLetterRepository, IMapper mapper, ILogger<UpdateCoverLetterCommandHandler> logger)
    {
        _coverLetterRepository = coverLetterRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateCoverLetterCommand request, CancellationToken cancellationToken)
    {
        var coverLetter = await _coverLetterRepository.GetByKeyAsync(request.CoverLetterId);
        if (coverLetter == null || coverLetter.IsDeleted)
        {
            throw new CoverLetterNotFoundException(request.CoverLetterId);
        }

        _mapper.Map(request.CoverLetter, coverLetter);
        await _coverLetterRepository.UpdateAsync(coverLetter.Id, coverLetter);

        _logger.LogInformation("Cover letter \"{CoverLetterId}\" is updated", coverLetter.Id);
            
        return Unit.Value;
    }
}