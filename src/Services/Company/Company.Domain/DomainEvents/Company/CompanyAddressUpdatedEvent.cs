using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;
using Company.Domain.ValueObjects;

namespace Company.Domain.DomainEvents.Company
{
    public class CompanyAddressUpdatedEvent : DomainEvent
    {
        private CompanyAddressUpdatedEvent() { } // for serialization
        
        public CompanyAddressUpdatedEvent(Entities.Company company)
        {
            Check.NotNull(company, nameof(company));

            CompanyId = company.Id;
            Address = company.AddressInfo;
        }

        public Guid CompanyId { get; private set; }
        public AddressInfo Address { get; private set; }
    }
}