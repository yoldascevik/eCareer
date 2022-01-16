using Career.Domain.Extensions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Education.Commands.Delete;

public class DeleteEducationCommandHandler : ICommandHandler<DeleteEducationCommand>
{
    private readonly ICVRepository _cvRepository;
    private readonly ILogger<DeleteEducationCommandHandler> _logger;

    public DeleteEducationCommandHandler(ICVRepository cvRepository, ILogger<DeleteEducationCommandHandler> logger)
    {
        _cvRepository = cvRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);

        var education = cv.Educations.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.EducationId);
        if (education == null)
            throw new EducationNotFoundException(request.EducationId);

        education.IsDeleted = true;
        await _cvRepository.UpdateAsync(cv.Id, cv);
            
        _logger.LogInformation("Education \"{EducationId}\" is deleted", education.Id);
            
        return Unit.Value;
    }
}