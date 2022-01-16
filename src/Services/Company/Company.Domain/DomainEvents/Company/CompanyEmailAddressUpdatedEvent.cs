using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Company.Domain.DomainEvents.Company;

public class CompanyEmailAddressUpdatedEvent : DomainEvent
{
    private CompanyEmailAddressUpdatedEvent(){} // for serialization
        
    public CompanyEmailAddressUpdatedEvent(Entities.Company company, string oldEmailAdress)
    {
        Check.NotNull(company, nameof(company));
        Check.NotNullOrEmpty(oldEmailAdress, nameof(oldEmailAdress));

        CompanyId = company.Id;
        EmailAddress = company.Email;
        OldEmailAddress = oldEmailAdress;
    }

    public Guid CompanyId { get; private set; }
    public string EmailAddress { get; private set; }
    public string OldEmailAddress { get; private set; }
}