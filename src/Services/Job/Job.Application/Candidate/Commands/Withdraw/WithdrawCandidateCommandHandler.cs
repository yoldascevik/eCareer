using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Job.Domain.CandidateAggregate.Repositories;
using Job.Domain.JobAggregate.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Job.Application.Candidate.Commands.Withdraw
{
    public class WithdrawCandidateCommandHandler : ICommandHandler<WithdrawCandidateCommand>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ILogger<WithdrawCandidateCommandHandler> _logger;

        public WithdrawCandidateCommandHandler(IJobRepository jobRepository,
            ICandidateRepository candidateRepository,
            ILogger<WithdrawCandidateCommandHandler> logger)
        {
            _logger = logger;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
        }

        public async Task<Unit> Handle(WithdrawCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId);
            if (candidate == null)
                throw new NotFoundException($"Candidate is not found by id: {request.CandidateId}");

            var job = await _jobRepository.GetByIdAsync(candidate.JobId);
            if (job == null)
                throw new NotFoundException($"Job is not found by id: {candidate.JobId}");

            job.WithdrawCandidate(candidate);
            await _jobRepository.UpdateAsync(job.Id, job);

            _logger.LogInformation("Candidate {CandidateId} is withrawn from job {JobId}", candidate.Id, job.Id);

            return Unit.Value;
        }
    }
}