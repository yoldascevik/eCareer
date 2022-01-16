using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.WorkExperience.Commands.Update;

public class UpdateWorkExperienceCommandHandler : ICommandHandler<UpdateWorkExperienceCommand>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;
    private readonly ILogger<UpdateWorkExperienceCommandHandler> _logger;

    public UpdateWorkExperienceCommandHandler(ICVRepository cvRepository, IMapper mapper, ILogger<UpdateWorkExperienceCommandHandler> logger)
    {
        _cvRepository = cvRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateWorkExperienceCommand request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);
            
        var workExperience = cv.WorkExperiences.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.WorkExperienceId);
        if (workExperience == null)
            throw new WorkExperienceNotFoundException(request.WorkExperienceId);

        _mapper.Map(request.WorkExperience, workExperience);
        await _cvRepository.UpdateAsync(cv.Id, cv);
        _logger.LogInformation("WorkExperience \"{WorkExperienceId}\" updated", request.WorkExperienceId);

        return Unit.Value;
    }
}