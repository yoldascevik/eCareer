using System;
using System.Collections.Generic;
using Career.Domain;
using Career.Domain.Entities;

namespace Job.Domain.JobApplicationAggregate
{
    public class JobApplication: DomainEntity, IAggregateRoot
    {
        public JobApplication()
        {
            IsActive = true;
        }
        
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CvId { get; set; }
        public string CoverLetter { get; set; }
        public string Channel { get; set; }
        public string Referance { get; set; }
        public DateTime ApplicationDate { get; set; }
        public bool IsActive { get; set; }
        
        #region DomainEntity Members

        protected override IEnumerable<object> GetIdentityMembers() => new[] {Id};

        #endregion
    }
}