using System;
using Career.Domain.Entities;
using Career.Exceptions;
using Career.Shared.Timing;
using Company.Domain.DomainEvents.CompanyFollower;

namespace Company.Domain.Entities
{
    public class CompanyFollower: DomainEntity
    {
        public CompanyFollower()
        {
            Company = null;
            Id = Guid.NewGuid();
        }
        
        public Guid Id { get; }
        public Guid CompanyId { get; private set; }
        public Guid UserId { get; private set;}
        public bool IsDeleted { get; private set;}
        public DateTime CreationTime { get; private set;}
        public DateTime? LastModificationTime { get; private set;}
        public Company Company { get; private set; }

        public static CompanyFollower Create(Company company, Guid userId)
        {
            Check.NotNull(company, nameof(company));
            Check.NotNull(userId, nameof(userId));
            
            var follower = new CompanyFollower()
            {
                UserId = userId,
                Company = company,
                CompanyId = company.Id,
                CreationTime = Clock.Now
            };
            
            follower.AddDomainEvent(new FollowerCreatedEvent(follower));
            return follower;
        }

        public void MarkDeleted()
        {
            IsDeleted = true;
            LastModificationTime = Clock.Now;
            
            AddDomainEvent(new FollowerDeletedEvent(this));
        }
    }
}