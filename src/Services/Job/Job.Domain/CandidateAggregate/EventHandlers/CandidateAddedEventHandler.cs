using System.Threading;
using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using Job.Domain.CandidateAggregate.Repositories;
using Job.Domain.JobAggregate.Events;

namespace Job.Domain.CandidateAggregate.EventHandlers
{
    public class CandidateAddedEventHandler : IDomainEventHandler<CandidateAddedEvent>
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateAddedEventHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task Handle(CandidateAddedEvent notification, CancellationToken cancellationToken)
        {
            await _candidateRepository.AddAsync(notification.Candidate);
        }
    }
}