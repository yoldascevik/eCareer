using Career.Domain.DomainEvent;
using Career.Exceptions;
using Job.Domain.CandidateAggregate;

namespace Job.Domain.JobAggregate.Events
{
    public class CandidateAddedEvent : DomainEvent
    {
        public CandidateAddedEvent(Job job, Candidate candidate)
        {
            Check.NotNull(job, nameof(job));
            Check.NotNull(candidate, nameof(candidate));

            Job = job;
            Candidate = candidate;
        }

        public Job Job { get; }
        public Candidate Candidate { get; }
    }
}