using Career.Domain.DomainEvent;
using Career.Exceptions;
using Job.Domain.CandidateAggregate;

namespace Job.Domain.JobAggregate.Events
{
    public class CandidateWithdrewEvent : DomainEvent
    {
        public CandidateWithdrewEvent(Job job, Candidate candidate)
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