using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Career.Domain;
using Career.Domain.Audit;
using Career.Domain.Entities;
using Career.Exceptions;
using Career.Exceptions.Exceptions;
using Company.Domain.Rules.Company;
using Company.Domain.Values;

namespace Company.Domain.Entities
{
    public class Company : DomainEntity<Guid>, IAggregateRoot, IAudited, ISoftDeletable
    {
        public Company()
        {
            Followers = new Collection<CompanyFollower>();
        }
        
        // private ctor
        private Company(string name, TaxInfo taxInfo, CompanyAddress address)
            :this()
        {
            Name = name;
            TaxInfo = taxInfo;
            Address = address;
        }

        #region Properties

        public string Name { get; }
        public TaxInfo TaxInfo { get; }
        public CompanyAddress Address { get; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string FaxNumber { get; set; }
        public int EmployeesCount { get; set; }
        public short EstablishedYear { get; set; }
        public bool IsDeleted { get; set; }
        public string SectorId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public virtual ICollection<CompanyFollower> Followers { get; }

        #endregion

        #region Methods

        public static Company Create(
            string name,
            TaxInfo taxInfo,
            CompanyAddress companyAddress,
            ICompanyTaxNumberUniquenessSpecification uniquenessSpecification)
        {
            var company = new Company(name, taxInfo, companyAddress);
            
            CheckRule(new CompanyTaxNumberMustBeUniqueRule(company, uniquenessSpecification));

            return company;
        }

        public Company Follow(Guid userId)
        {
            Check.NotNull(userId, nameof(userId));

            var companyFollower = Followers.FirstOrDefault(c => c.UserId == userId);
            if (companyFollower != null && !companyFollower.IsDeleted)
            {
                throw new AlreadyExistsException($"User already follow the company.");
            }
            else if (companyFollower != null && companyFollower.IsDeleted)
            {
                companyFollower.IsDeleted = false;
            }
            else
            {
                Followers.Add(new CompanyFollower()
                {
                    CompanyId = Id,
                    UserId = userId
                });
            }

            return this;
        }

        #endregion
    }
}