using System;
using Career.Domain.Entities;
using Career.Exceptions;
using Job.Domain.CandidateAggregate;
using Job.Domain.CandidateAggregate.Rules;

namespace Job.Domain.JobAggregate
{
    public class CandidateRef: DomainEntity
    {
        public Guid Id { get; private init; }
        public Guid UserId { get; private init; }
        public bool IsActive { get; private set; }
        
        public static CandidateRef Create(Candidate candidate)
        {
            Check.NotNull(candidate, nameof(candidate));
            
            return new CandidateRef()
            {
                Id = candidate.Id,
                UserId =  candidate.UserId,
                IsActive = candidate.IsActive
            };
        }
        
        internal void Withdraw()
        {
            CheckRule(new CandidateMustBeActiveRule(IsActive));
            IsActive = false;
        }
    }
}