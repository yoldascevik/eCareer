using System;
using Career.Domain;
using Career.Domain.Entities;
using Career.Exceptions;
using Career.Shared.Timing;
using Job.Domain.CandidateAggregate.Rules;

namespace Job.Domain.CandidateAggregate;

public class Candidate : DomainEntity, IAggregateRoot
{
    public Candidate()
    {
        Id = Guid.NewGuid();
        IsActive = true;
    }

    public Guid Id { get; private set; }
    public Guid JobId { get; private init; }
    public Guid UserId { get; private init; }
    public Guid CvId { get; private init; }
    public string CoverLetter { get; private init; }
    public string Channel { get; private init; }
    public string Referance { get; private init; }
    public DateTime ApplicationDate { get; private init; }
    public DateTime? WithdrawalDate { get; private set; }
    public bool IsActive { get; private set; }

    public static Candidate Create(JobAggregate.Job job, Guid userId, Guid cvId, string coverLetter, string channel = null, string referance = null)
    {
        Check.NotNull(job, nameof(job));
        Check.NotEmpty(cvId, nameof(cvId));
        Check.NotEmpty(userId, nameof(userId));

        Candidate candidate = new()
        {
            JobId = job.Id,
            UserId = userId,
            CvId = cvId,
            Channel = channel,
            Referance = referance,
            CoverLetter = coverLetter,
            ApplicationDate = Clock.Now
        };
            
        return candidate;
    }

    internal void Withdrawn()
    {
        CheckRule(new CandidateMustBeActiveRule(IsActive));
            
        IsActive = false;
        WithdrawalDate = Clock.Now;
    }
}