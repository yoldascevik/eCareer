using System;
using System.Collections.Generic;
using Career.Domain;
using Career.Domain.Entities;
using Career.Exceptions;
using Career.Shared.Timing;
using Company.Domain.Rules.Company;
using Company.Domain.Rules.CompanyFollower;
using Company.Domain.Values;

namespace Company.Domain.Entities
{
    public class Company : DomainEntity<Guid>, IAggregateRoot
    {
        #region Ctor

        private Company()
        {
            Followers = new List<CompanyFollower>();
        }

        #endregion
        
        #region Properties
        
        public string Name { get; private set; }
        public TaxInfo TaxInfo { get; private set; }
        public AddressInfo AddressInfo { get; private set; }
        public string Website { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string MobilePhone { get; private set; }
        public string FaxNumber { get; private set; }
        public int EmployeesCount { get; private set; }
        public short EstablishedYear { get; private set; }
        public bool IsDeleted { get; private set; }
        public string SectorId { get; private set; }
        public DateTime CreationTime { get; private set; }
        public long? CreatorUserId { get; private set; }
        public DateTime? LastModificationTime { get; private set; }
        public long? LastModifierUserId { get; private set; }
        public ICollection<CompanyFollower> Followers { get; }

        #endregion

        #region Methods

        public static Company Create(string name, string email, TaxInfo taxInfo, AddressInfo addressInfo, string webSite, 
            string phone, string mobilePhone, string faxNumber, int employeesCount, short establishedYear, string sectorId, 
            ITaxNumberUniquenessSpecification taxNumberUniquenessSpec, IEmailAddressUniquenessSpecification emailAddressUniquenessSpec)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            Check.NotNull(addressInfo, nameof(addressInfo));

            CheckRule(new TaxNumberMustBeUniqueRule(taxInfo, taxNumberUniquenessSpec));
            CheckRule(new EmailAddressMustBeUniqueRule(email, emailAddressUniquenessSpec));
            CheckRule(new SectorIdRequiredRule(sectorId));
            CheckRule(new PhoneRequiredRule(phone));
            
            return new Company
            {
                Name = name,
                Email = email,
                TaxInfo = taxInfo,
                AddressInfo = addressInfo,
                Phone = phone,
                Website = webSite,
                FaxNumber = faxNumber,
                MobilePhone = mobilePhone,
                SectorId = sectorId,
                EmployeesCount = employeesCount,
                EstablishedYear = establishedYear,
                CreationTime = Clock.Now,
                CreatorUserId = null, // TODO
                LastModificationTime = Clock.Now,
                LastModifierUserId = null // TODO
            };
        }

        public Company Follow(Guid userId, ICompanyFollowerUniquenessSpecification uniquenessSpecification)
        {
            var companyFollower = CompanyFollower.Create(this, userId);

            CheckRule(new CompanyFollowerMustBeUniqueRule(companyFollower, uniquenessSpecification));

            Followers.Add(companyFollower);
            return this;
        }
        
        public Company Unfollow(CompanyFollower companyFollower)
        {
            Check.NotNull(companyFollower, nameof(companyFollower));
            companyFollower.MarkDeleted();
            return this;
        }

        public void UpdateName(string companyName)
        {
            Name = companyName;
            LastModificationTime = Clock.Now;
            LastModifierUserId = null; // TODO
        }

        public void UpdateTaxInfo(TaxInfo taxInfo, ITaxNumberUniquenessSpecification taxNumberUniquenessSpec)
        {
            CheckRule(new TaxNumberMustBeUniqueRule(taxInfo, taxNumberUniquenessSpec));
            
            TaxInfo = taxInfo;
            LastModificationTime = Clock.Now;
            LastModifierUserId = null; // TODO
        }

        public void UpdateAddress(AddressInfo address)
        {
            Check.NotNull(address, nameof(address));
            
            AddressInfo = address;
            LastModificationTime = Clock.Now;
            LastModifierUserId = null; // TODO
        }

        public void UpdateEmailAddress(string email, IEmailAddressUniquenessSpecification emailAddressUniquenessSpec)
        {
            CheckRule(new EmailAddressMustBeUniqueRule(email, emailAddressUniquenessSpec));
            
            Email = email;
            LastModificationTime = Clock.Now;
            LastModifierUserId = null; // TODO
        }
        
        public void UpdateDetails(string phone, string mobilePhone, string faxNumber, 
            string website, int employeesCount, short establishedYear, string sectorId)
        {
            CheckRule(new SectorIdRequiredRule(sectorId));
            CheckRule(new PhoneRequiredRule(phone));
            
            Phone = phone;
            MobilePhone = mobilePhone;
            FaxNumber = faxNumber;
            Website = website;
            EmployeesCount = employeesCount;
            EstablishedYear = establishedYear;
            SectorId = sectorId;
            LastModificationTime = Clock.Now;
            LastModifierUserId = null; //TODO
        }

        public void MarkDeleted()
        {
            IsDeleted = true;
            LastModificationTime = Clock.Now;
            LastModifierUserId = null; // TODO
        }

        #endregion
    }
}