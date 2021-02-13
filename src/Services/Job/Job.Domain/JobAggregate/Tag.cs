using System;
using System.Collections.Generic;
using Career.Domain.Entities;

namespace Job.Domain.JobAggregate
{
    public class Tag : DomainEntity
    {
        public Tag()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }
        public string Name { get; set; }

        #region DomainEntity Members

        protected override IEnumerable<object> GetIdentityMembers() => new[] {Id};

        #endregion
    }
}