using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.Extensions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.WorkExperience.Commands.Delete;

public class DeleteWorkExperienceCommandHandler : ICommandHandler<DeleteWorkExperienceCommand>
{
    private readonly ICVRepository _cvRepository;
    private readonly ILogger<DeleteWorkExperienceCommandHandler> _logger;

    public DeleteWorkExperienceCommandHandler(ICVRepository cvRepository, ILogger<DeleteWorkExperienceCommandHandler> logger)
    {
        _cvRepository = cvRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteWorkExperienceCommand request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);

        var workExperience = cv.WorkExperiences.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.WorkExperienceId);
        if (workExperience == null)
            throw new WorkExperienceNotFoundException(request.WorkExperienceId);

        workExperience.IsDeleted = true;
        await _cvRepository.UpdateAsync(cv.Id, cv);
            
        _logger.LogInformation("WorkExperience \"{WorkExperienceId}\" is deleted", workExperience.Id);
            
        return Unit.Value;
    }
}