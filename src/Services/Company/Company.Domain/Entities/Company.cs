using System;
using System.Collections.Generic;
using System.Linq;
using Career.Domain;
using Career.Domain.Entities;
using Career.Exceptions;
using Career.Shared.Timing;
using Company.Domain.DomainEvents.Company;
using Company.Domain.Refs;
using Company.Domain.Rules.Company;
using Company.Domain.Rules.CompanyAddress;
using Company.Domain.Rules.CompanyFollower;
using Company.Domain.ValueObjects;

namespace Company.Domain.Entities;

public class Company : DomainEntity, IAggregateRoot
{
    #region Fields

    private List<Address> _addresses;
    private List<CompanyFollower> _followers;

    #endregion
        
    #region Ctor

    private Company()
    {
        Id = Guid.NewGuid();
        _addresses = new List<Address>();
        _followers = new List<CompanyFollower>();
    }

    #endregion
        
    #region Properties

    public Guid Id { get; }
    public string Name { get; private set; }
    public string Website { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string MobilePhone { get; private set; }
    public string FaxNumber { get; private set; }
    public int EmployeesCount { get; private set; }
    public short EstablishedYear { get; private set; }
    public TaxInfo TaxInfo { get; private set; }
    public SectorRef Sector { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreationTime { get; private set; }
    public long? CreatorUserId { get; private set; }
    public DateTime? LastModificationTime { get; private set; }
    public long? LastModifiedUserId { get; private set; }
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
    public ICollection<CompanyFollower> Followers => _followers.AsReadOnly();

    #endregion

    #region Methods

    public static Company Create(string name, string email, TaxInfo taxInfo, string phone, SectorRef sectorRef, 
        ITaxNumberUniquenessSpecification taxNumberUniquenessSpec, IEmailAddressUniquenessSpecification emailAddressUniquenessSpec)
    {
        Check.NotNullOrEmpty(name, nameof(name));

        CheckRule(new TaxNumberMustBeUniqueRule(taxInfo, taxNumberUniquenessSpec));
        CheckRule(new EmailAddressMustBeUniqueRule(email, emailAddressUniquenessSpec));
        CheckRule(new SectorInfoRequiredRule(sectorRef));
        CheckRule(new PhoneRequiredRule(phone));

        var company = new Company
        {
            Name = name,
            Email = email,
            TaxInfo = taxInfo,
            Phone = phone,
            Sector = sectorRef,
            CreationTime = Clock.Now,
            CreatorUserId = null, // TODO
            LastModificationTime = Clock.Now,
            LastModifiedUserId = null // TODO
        };
            
        company.AddDomainEvent(new CompanyCreatedEvent(company));
        return company;
    }

    public Company Follow(Guid userId, ICompanyFollowerUniquenessSpecification uniquenessSpecification)
    {
        var companyFollower = CompanyFollower.Create(this, userId);

        CheckRule(new CompanyFollowerMustBeUniqueRule(companyFollower, uniquenessSpecification));

        _followers.Add(companyFollower);
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
        string oldCompanyName = Name;
            
        Name = companyName;
        LastModificationTime = Clock.Now;
        LastModifiedUserId = null; // TODO
            
        AddDomainEvent(new CompanyNameUpdatedEvent(this, oldCompanyName));
    }

    public void UpdateTaxInfo(TaxInfo taxInfo, ITaxNumberUniquenessSpecification taxNumberUniquenessSpec)
    {
        CheckRule(new TaxNumberMustBeUniqueRule(taxInfo, taxNumberUniquenessSpec));
            
        TaxInfo = taxInfo;
        LastModificationTime = Clock.Now;
        LastModifiedUserId = null; // TODO
            
        AddDomainEvent(new CompanyTaxInfoUpdatedEvent(this));
    }
        
    public void UpdateEmailAddress(string email, IEmailAddressUniquenessSpecification emailAddressUniquenessSpec)
    {
        CheckRule(new EmailAddressMustBeUniqueRule(email, emailAddressUniquenessSpec));

        string oldEmailAdress = Email;
        Email = email;
        LastModificationTime = Clock.Now;
        LastModifiedUserId = null; // TODO
            
        AddDomainEvent(new CompanyEmailAddressUpdatedEvent(this, oldEmailAdress));
    }
        
    public void UpdateDetails(string phone, string mobilePhone, string faxNumber, 
        string website, int employeesCount, short establishedYear, SectorRef sectorRef)
    {
        CheckRule(new SectorInfoRequiredRule(sectorRef));
        CheckRule(new PhoneRequiredRule(phone));
            
        Phone = phone;
        MobilePhone = mobilePhone;
        FaxNumber = faxNumber;
        Website = website;
        EmployeesCount = employeesCount;
        EstablishedYear = establishedYear;
        Sector = sectorRef;

        LastModificationTime = Clock.Now;
        LastModifiedUserId = null; //TODO
            
        AddDomainEvent(new CompanyDetailInfoUpdatedEvent(this));
    }

    public void AddAddress(Address address)
    {
        Check.NotNull(address, nameof(address));

        Address primaryAddress = Addresses.SingleOrDefault(x => x.IsPrimary);
        if (primaryAddress == null)
        {
            address.SetPrimary(true);
        }
        else if(address.IsPrimary)
        {
            primaryAddress.SetPrimary(false);
        }
            
        _addresses.Add(address);
    }

    public void SetPrimaryAddress(Address address)
    {
        Check.NotNull(address, nameof(address));

        Address primaryAddress = Addresses.SingleOrDefault(x => x.IsPrimary);
        if (primaryAddress != null && primaryAddress.Id != address.Id)
        {
            primaryAddress.SetPrimary(false);
        }

        address.SetPrimary(true);
    }
        
    public void RemoveAddress(Address address)
    {
        Check.NotNull(address, nameof(address));

        CheckRule(new PrimaryAddressCannotBeDeleteRule(address));
        address.MarkAsDeleted();
    }
        
    public void MarkDeleted()
    {
        IsDeleted = true;
        LastModificationTime = Clock.Now;
        LastModifiedUserId = null; // TODO
            
        AddDomainEvent(new CompanyDeletedEvent(this));
    }
        
    #endregion
}