using System;
using Career.Shared.Entities;
using Career.Shared.Interfaces;

namespace Company.Domain.Entities
{
    public class CompanyFollower: IEntity<Guid>, ISoftDeletable
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        public Company Company { get; set; }
    }
}