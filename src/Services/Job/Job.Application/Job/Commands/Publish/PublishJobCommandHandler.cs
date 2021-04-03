using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Job.Application.Job.Commands.Publish;
using Job.Domain.JobAggregate.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.PublishJob
{
    public class PublishJobCommandHandler: ICommandHandler<PublishJobCommand>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<PublishJobCommandHandler> _logger;

        public PublishJobCommandHandler(IJobRepository jobRepository, ILogger<PublishJobCommandHandler> logger)
        {
            _jobRepository = jobRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(PublishJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                throw new NotFoundException($"Job is not found by id: {request.JobId}");

            job.Publish(request.ValidityDate);
            await _jobRepository.UpdateAsync(job.Id, job);

            _logger.LogInformation("Job is published : {JobId}", job.Id);
            
            return Unit.Value;
        }
    }
}