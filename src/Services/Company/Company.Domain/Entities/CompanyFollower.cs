using System;
using Career.Domain;
using Career.Domain.Audit;
using Career.Domain.Entities;
using Career.Exceptions;
using Career.Shared.Timing;

namespace Company.Domain.Entities
{
    public class CompanyFollower: DomainEntity<Guid>, ISoftDeletable, IHasCreationTime, IHasModificationTime //TODO: set private public setter properties
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Company Company { get; set; }

        public static CompanyFollower Create(Company company, Guid userId)
        {
            Check.NotNull(company, nameof(company));
            Check.NotNull(userId, nameof(userId));
            
            return new CompanyFollower()
            {
                Company = company,
                UserId = userId,
                CreationTime = Clock.Now
            };
        }
    }
}