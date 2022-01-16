using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.Apply;

public class ApplyCommandHandler : ICommandHandler<ApplyCommand>
{
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<ApplyCommandHandler> _logger;

    public ApplyCommandHandler(IJobRepository jobRepository, ILogger<ApplyCommandHandler> logger)
    {
        _jobRepository = jobRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(ApplyCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdAsync(request.JobId);
        if (job is null)
            throw new JobNotFoundException(request.JobId);

        var candidate = Domain.CandidateAggregate.Candidate.Create(
            job,
            request.CandidateDto.UserId,
            request.CandidateDto.CvId,
            request.CandidateDto.CoverLetter,
            request.CandidateDto.Channel,
            request.CandidateDto.Referance);
            
        job.Apply(candidate);
        await _jobRepository.UpdateAsync(job.Id, job);

        _logger.LogInformation("Candidate {UserID} applied for job {JobId}", candidate.UserId, job.Id);

        return Unit.Value;
    }
}