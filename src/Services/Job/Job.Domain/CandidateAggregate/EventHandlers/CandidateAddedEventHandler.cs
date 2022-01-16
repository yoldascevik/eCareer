using Career.CAP.DomainEvent;
using DotNetCore.CAP;
using Job.Domain.CandidateAggregate.Repositories;
using Job.Domain.JobAggregate.Events;

namespace Job.Domain.CandidateAggregate.EventHandlers;

public class CandidateAddedEventHandler : CAPDomainEventHandler<CandidateAddedEvent>
{
    private readonly ICandidateRepository _candidateRepository;

    public CandidateAddedEventHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    [CapSubscribe(nameof(CandidateAddedEvent))]
    public override async Task Handle(CandidateAddedEvent domainEvent)
    {
        await _candidateRepository.AddAsync(domainEvent.Candidate);
    }
}