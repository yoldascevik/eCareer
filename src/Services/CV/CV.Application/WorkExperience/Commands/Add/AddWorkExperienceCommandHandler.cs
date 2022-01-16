using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Career.Shared.Generators;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.WorkExperience.Dtos;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.WorkExperience.Commands.Add;

public class AddWorkExperienceCommandHandler : ICommandHandler<AddWorkExperienceCommand, WorkExperienceDto>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;
    private readonly IStringIdGenerator _stringIdGenerator;
    private readonly ILogger<AddWorkExperienceCommandHandler> _logger;

    public AddWorkExperienceCommandHandler(
        ICVRepository cvRepository, 
        IMapper mapper, 
        IStringIdGenerator stringIdGenerator,
        ILogger<AddWorkExperienceCommandHandler> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _cvRepository = cvRepository;
        _stringIdGenerator = stringIdGenerator;
    }

    public async Task<WorkExperienceDto> Handle(AddWorkExperienceCommand request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);
  
        var workExperience = _mapper.Map<Core.Entities.WorkExperience>(request.WorkExperience);
        workExperience.Id = _stringIdGenerator.Generate();

        cv.WorkExperiences.Add(workExperience);

        await _cvRepository.UpdateAsync(cv.Id, cv);
        _logger.LogInformation("New workExperience ({WorkExperienceId}) added to CV ({CvId})", workExperience.Id, cv.Id);

        return _mapper.Map<WorkExperienceDto>(workExperience);
    }
}