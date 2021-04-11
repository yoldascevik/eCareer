using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.Revoke
{
    public class RevokeJobCommandHandler: ICommandHandler<RevokeJobCommand>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<RevokeJobCommandHandler> _logger;

        public RevokeJobCommandHandler(IJobRepository jobRepository, ILogger<RevokeJobCommandHandler> logger)
        {
            _jobRepository = jobRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(RevokeJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                throw new JobNotFoundException(request.JobId);

            job.Revoke(request.Reason);
            await _jobRepository.UpdateAsync(job.Id, job);

            _logger.LogInformation("Job is revoked : {JobId}", job.Id);
            
            return Unit.Value;
        }
    }
}