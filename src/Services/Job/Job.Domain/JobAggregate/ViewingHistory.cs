using System;
using Career.Domain.Entities;

namespace Job.Domain.JobAggregate
{
    public class ViewingHistory: DomainEntity
    {
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime ViewingDate { get; set; }
        public string Channel { get; set; }
        public string Referance { get; set; }
    }
}