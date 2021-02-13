using System.Collections.Generic;
using Career.Domain.Entities;

namespace Job.Domain.JobAggregate
{
    public class WorkType: DomainEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        #region DomainEntity Members

        protected override IEnumerable<object> GetIdentityMembers() => new[] {Id};

        #endregion
    }
}