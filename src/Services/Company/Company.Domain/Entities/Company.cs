using System;
using System.Collections.Generic;
using Career.Shared.Audit;
using Career.Shared.Entities;
using Career.Shared.Interfaces;

namespace Company.Domain.Entities
{
    public class Company : IEntity<Guid>, IAudited, ISoftDeletable
    {
        public Guid Id { get; set; }
        public string SectorId { get; set; }
        public string CountryId { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string FaxNumber { get; set; }
        public string Address { get; set; }
        public int EmployeesCount { get; set; }
        public short EstablishedYear { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }

        public ICollection<CompanyFollower> Followers { get; set; }
    }
}