using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
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
                throw new NotFoundException($"Job is not found by id: {request.JobId}");

            job.MarkAsDelete();
            await _jobRepository.UpdateAsync(job.Id, job);

            _logger.LogInformation("Job is deleted : {JobId}", job.Id);

            return Unit.Value;
        }
    }
}