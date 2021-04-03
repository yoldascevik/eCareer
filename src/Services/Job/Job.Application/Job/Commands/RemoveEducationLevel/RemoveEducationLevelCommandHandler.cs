using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Job.Domain.JobAggregate.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.RemoveEducationLevel
{
    public class RemoveEducationLevelCommandHandler: ICommandHandler<RemoveEducationLevelCommand>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<RemoveEducationLevelCommandHandler> _logger;

        public RemoveEducationLevelCommandHandler(IJobRepository jobRepository, ILogger<RemoveEducationLevelCommandHandler> logger)
        {
            _logger = logger;
            _jobRepository = jobRepository;
        }

        public async Task<Unit> Handle(RemoveEducationLevelCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                throw new NotFoundException($"Job is not found by id: {request.JobId}");

            job.RemoveEducationLevel(request.EducationLevelId);
            await _jobRepository.UpdateAsync(job.Id, job);
            
            _logger.LogInformation("Education level {EducationLevelId} removed from job: {JobId}", request.EducationLevelId, job.Id);
            
            return Unit.Value;
        }
    }
}