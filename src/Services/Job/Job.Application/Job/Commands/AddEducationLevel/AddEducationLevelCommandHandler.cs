using AutoMapper;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Refs;
using Job.Domain.JobAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.AddEducationLevel;

public class AddEducationLevelCommandHandler : ICommandHandler<AddEducationLevelCommand, IdNameRefDto>
{
    private readonly IMapper _mapper;
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<AddEducationLevelCommandHandler> _logger;

    public AddEducationLevelCommandHandler(IJobRepository jobRepository, IMapper mapper, ILogger<AddEducationLevelCommandHandler> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _jobRepository = jobRepository;
    }

    public async Task<IdNameRefDto> Handle(AddEducationLevelCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdAsync(request.JobId);
        if (job is null) 
            throw new JobNotFoundException(request.JobId);

        var educationLevel = EducationLevelRef.Create(request.EducationLevelDto.RefId, request.EducationLevelDto.Name);
        job.AddEducationLevel(educationLevel);

        await _jobRepository.UpdateAsync(job.Id, job);
        _logger.LogInformation("New education level added to job: {JobId}", job.Id);

        return _mapper.Map<IdNameRefDto>(educationLevel);
    }
}