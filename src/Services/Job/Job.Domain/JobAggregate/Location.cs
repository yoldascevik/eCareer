using System.Collections.Generic;
using Career.Domain.Entities;

namespace Job.Domain.JobAggregate
{
    public class Location: DomainEntity
    {
        public string Id { get; set; }
        public string CountryId { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
        
        #region DomainEntity Members

        protected override IEnumerable<object> GetIdentityMembers() => new[] {Id};

        #endregion
    }
}