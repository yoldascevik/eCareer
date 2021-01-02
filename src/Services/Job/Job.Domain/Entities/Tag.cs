using System;
using System.Collections.Generic;
using Career.Domain.Entities;

namespace Job.Domain.Entities
{
    public class Tag: DomainEntity<Guid>
    {
        public Tag()
        {
            Tags = new List<JobTag>();
        }
        
        public string Name { get; set; }
        public ICollection<JobTag> Tags { get; set; }
    }
}