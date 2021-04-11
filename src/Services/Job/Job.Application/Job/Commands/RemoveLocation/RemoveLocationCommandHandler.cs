using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.RemoveLocation
{
    public class RemoveLocationCommandHandler: ICommandHandler<RemoveLocationCommand>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<RemoveLocationCommandHandler> _logger;

        public RemoveLocationCommandHandler(IJobRepository jobRepository, ILogger<RemoveLocationCommandHandler> logger)
        {
            _logger = logger;
            _jobRepository = jobRepository;
        }

        public async Task<Unit> Handle(RemoveLocationCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                throw new JobNotFoundException(request.JobId);

            job.RemoveLocation(request.LocationId);
            await _jobRepository.UpdateAsync(job.Id, job);
            
            _logger.LogInformation("Job location {LocationId} removed from job: {JobId}", request.LocationId, job.Id);

            return Unit.Value;
        }
    }
}