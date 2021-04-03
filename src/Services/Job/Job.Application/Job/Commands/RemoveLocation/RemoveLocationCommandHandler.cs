using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
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
                throw new NotFoundException($"Job is not found by id: {request.JobId}");

            job.RemoveLocation(request.LocationId);
            await _jobRepository.UpdateAsync(job.Id, job);
            
            _logger.LogInformation("New location added to job: {JobId}", job.Id);

            return Unit.Value;
        }
    }
}