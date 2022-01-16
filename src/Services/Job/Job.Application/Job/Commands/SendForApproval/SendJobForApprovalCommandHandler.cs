using Career.MediatR.Command;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.SendForApproval;

public class SendJobForApprovalCommandHandler: ICommandHandler<SendJobForApprovalCommand>
{
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<SendJobForApprovalCommandHandler> _logger;

    public SendJobForApprovalCommandHandler(IJobRepository jobRepository, ILogger<SendJobForApprovalCommandHandler> logger)
    {
        _jobRepository = jobRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(SendJobForApprovalCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdAsync(request.JobId);
        if (job is null)
            throw new JobNotFoundException(request.JobId);

        job.SendForApproval();
        await _jobRepository.UpdateAsync(job.Id, job);

        _logger.LogInformation("Job sent for approval : {JobId}", job.Id);
            
        return Unit.Value;
    }
}