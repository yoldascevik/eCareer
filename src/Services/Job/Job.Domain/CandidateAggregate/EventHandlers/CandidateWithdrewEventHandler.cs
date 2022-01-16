using System.Threading.Tasks;
using Career.CAP.DomainEvent;
using Career.Exceptions.Exceptions;
using DotNetCore.CAP;
using Job.Domain.CandidateAggregate.Repositories;
using Job.Domain.JobAggregate.Events;

namespace Job.Domain.CandidateAggregate.EventHandlers;

public class CandidateWithdrewEventHandler : CAPDomainEventHandler<CandidateWithdrewEvent>
{
    private readonly ICandidateRepository _candidateRepository;

    public CandidateWithdrewEventHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    [CapSubscribe(nameof(CandidateWithdrewEvent))]
    public override async Task Handle(CandidateWithdrewEvent domainEvent)
    {
        Candidate candidate = await _candidateRepository.GetByIdAsync(domainEvent.Candidate.Id);
        if (candidate == null)
        {
            throw new NotFoundException($"Candidate not found ({domainEvent.Candidate.Id}).");
        }

        candidate.Withdrawn();
        await _candidateRepository.UpdateAsync(candidate.Id, candidate);
    }
}