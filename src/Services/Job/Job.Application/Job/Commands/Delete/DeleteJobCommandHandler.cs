using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.Delete
{
    public class DeleteJobCommandHandler: ICommandHandler<DeleteJobCommand>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<DeleteJobCommandHandler> _logger;

        public DeleteJobCommandHandler(IJobRepository jobRepository, ILogger<DeleteJobCommandHandler> logger)
        {
            _jobRepository = jobRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                throw new JobNotFoundException(request.JobId);

            job.MarkAsDelete();
            await _jobRepository.UpdateAsync(job.Id, job);

            _logger.LogInformation("Job is deleted : {JobId}", job.Id);

            return Unit.Value;
        }
    }
}