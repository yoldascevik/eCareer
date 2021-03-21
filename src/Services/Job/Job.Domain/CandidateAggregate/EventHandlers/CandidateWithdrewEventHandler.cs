using System.Threading;
using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using Career.Exceptions.Exceptions;
using Job.Domain.CandidateAggregate.Repositories;
using Job.Domain.JobAggregate.Events;

namespace Job.Domain.CandidateAggregate.EventHandlers
{
    public class CandidateWithdrewEventHandler : IDomainEventHandler<CandidateWithdrewEvent>
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateWithdrewEventHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task Handle(CandidateWithdrewEvent notification, CancellationToken cancellationToken)
        {
            Candidate candidate = await _candidateRepository.GetByIdAsync(notification.Candidate.Id);
            if (candidate == null)
            {
                throw new NotFoundException($"Candidate not found ({notification.Candidate.Id}).");
            }

            candidate.Withdrawn();
            await _candidateRepository.UpdateAsync(candidate.Id, candidate);
        }
    }
}