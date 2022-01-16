using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Refs;
using Job.Domain.JobAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.AddWorkType;

public class AddWorkTypeCommandHandler: ICommandHandler<AddWorkTypeCommand, IdNameRefDto>
{
    private readonly IMapper _mapper;
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<AddWorkTypeCommandHandler> _logger;

    public AddWorkTypeCommandHandler(IJobRepository jobRepository, IMapper mapper, ILogger<AddWorkTypeCommandHandler> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _jobRepository = jobRepository;
    }

    public async Task<IdNameRefDto> Handle(AddWorkTypeCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdAsync(request.JobId);
        if (job is null)
            throw new JobNotFoundException(request.JobId);

        var workType = WorkTypeRef.Create(request.WorkTypeDto.RefId, request.WorkTypeDto.Name);
        job.AddWorkType(workType);

        await _jobRepository.UpdateAsync(job.Id, job);
        _logger.LogInformation("New work type added to job: {JobId}", job.Id);

        return _mapper.Map<IdNameRefDto>(workType);
    }
}