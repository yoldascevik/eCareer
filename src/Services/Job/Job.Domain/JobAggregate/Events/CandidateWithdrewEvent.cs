using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;
using Job.Domain.CandidateAggregate;

namespace Job.Domain.JobAggregate.Events
{
    public class CandidateWithdrewEvent : DomainEvent
    {
        private CandidateWithdrewEvent(){} // for serialization
        
        public CandidateWithdrewEvent(Job job, Candidate candidate)
        {
            Check.NotNull(job, nameof(job));
            Check.NotNull(candidate, nameof(candidate));

            JobId = job.Id;
            Candidate = candidate;
        }

        public Guid JobId { get; private set; }
        public Candidate Candidate { get; private set; }
    }
}