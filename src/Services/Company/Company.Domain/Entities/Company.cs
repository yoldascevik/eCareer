using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Career.Domain;
using Career.Shared.Audit;
using Career.Shared.Interfaces;
using Company.Domain.Rules.Company;
using Company.Domain.Values;

namespace Company.Domain.Entities
{
    public class Company : Entity<Guid>, IAggregateRoot, IAudited, ISoftDeletable
    {
        // private ctor
        private Company(string name, TaxInfo taxInfo, CompanyAddress address)
        {
            Name = name;
            TaxInfo = taxInfo;
            Address = address;
            Followers = new Collection<CompanyFollower>();
        }

        #region Properties

        public string Name { get; }
        public TaxInfo TaxInfo { get; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string FaxNumber { get; set; }
        public int EmployeesCount { get; set; }
        public short EstablishedYear { get; set; }
        public bool IsDeleted { get; set; }
        public string SectorId { get; set; }
        public CompanyAddress Address { get; }    
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public ICollection<CompanyFollower> Followers { get; }

        #endregion

        #region Methods

        public static Company Create(
            string name, 
            TaxInfo taxInfo, 
            CompanyAddress companyAddress,
            ICompanyTaxNumberUniquenessSpecification uniquenessSpecification)
        {
            CheckRule(new CompanyTaxNumberMustBeUniqueRule(taxInfo,uniquenessSpecification));
            
            return new Company(name, taxInfo, companyAddress);
        }
        
        #endregion
    }
}