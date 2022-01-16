using System;
using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Job.Domain.JobAggregate.Refs;
using Job.Domain.JobAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.Create;

public class CreateJobCommandHandler: ICommandHandler<CreateJobCommand, Guid>
{
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<CreateJobCommandHandler> _logger;
        
    public CreateJobCommandHandler(IJobRepository jobRepository, ILogger<CreateJobCommandHandler> logger)
    {
        _jobRepository = jobRepository;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var job = Domain.JobAggregate.Job.Create(
                CompanyRef.Create(request.Company.RefId, request.Company.Name), 
                request.Job.Title,
                request.Job.Description,
                SectorRef.Create(request.Job.Sector.RefId, request.Job.Sector.Name), 
                JobPositionRef.Create(request.Job.JobPosition.RefId, request.Job.JobPosition.Name), 
                LanguageRef.Create(request.Job.Language.RefId, request.Job.Language.Name))
            .SetMinExperienceYear(request.Job.MinExperienceYear)
            .SetMaxExperienceYear(request.Job.MaxExperienceYear)
            .SetCanDisabilities(request.Job.IsCanDisabilities)
            .SetPersonCount(request.Job.PersonCount)
            .SetGender(request.Job.Gender);

        await _jobRepository.AddAsync(job);
        _logger.LogInformation("Created new job : \"{JobTitle}\" - \"{JobId}\" by company \"{@Company}\"", job.Title, job.Id, job.Company);
            
        return job.Id;
    }
}