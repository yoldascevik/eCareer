using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Company.Domain.DomainEvents.Company
{
    public class CompanyEmailAddressUpdatedEvent : DomainEvent, IDomainEvent
    {
        public CompanyEmailAddressUpdatedEvent(Entities.Company company, string oldEmailAdress)
        {
            Check.NotNull(company, nameof(company));
            Check.NotNullOrEmpty(oldEmailAdress, nameof(oldEmailAdress));

            CompanyId = company.Id;
            EmailAddress = company.Email;
            OldEmailAddress = oldEmailAdress;
        }

        public Guid CompanyId { get; }
        public string EmailAddress { get; }
        public string OldEmailAddress { get; }
    }
}