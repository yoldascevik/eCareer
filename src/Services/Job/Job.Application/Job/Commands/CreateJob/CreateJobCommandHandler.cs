using System;
using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Job.Domain.JobAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.CreateJob
{
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
                    request.CompanyId,
                    request.Job.Title,
                    request.Job.Description,
                    request.Job.SectorId,
                    request.Job.JobPositionId,
                    request.Job.LanguageId)
                .SetMinExperienceYear(request.Job.MinExperienceYear)
                .SetMaxExperienceYear(request.Job.MaxExperienceYear)
                .SetCanDisabilities(request.Job.IsCanDisabilities)
                .SetPersonCount(request.Job.PersonCount)
                .SetGender(request.Job.Gender);

            await _jobRepository.AddAsync(job);
            _logger.LogInformation("Created new job : \"{JobTitle}\" - \"{JobId}\" by company \"{CompanyId}\"", job.Title, job.Id, job.CompanyId);
            
            return job.Id;
        }
    }
}