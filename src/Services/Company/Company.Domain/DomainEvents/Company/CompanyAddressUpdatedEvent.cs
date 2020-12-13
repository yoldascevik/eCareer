using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;
using Company.Domain.Values;

namespace Company.Domain.DomainEvents.Company
{
    public class CompanyAddressUpdatedEvent : DomainEvent, IDomainEvent
    {
        public CompanyAddressUpdatedEvent(Entities.Company company)
        {
            Check.NotNull(company, nameof(company));

            CompanyId = company.Id;
            Address = company.AddressInfo;
        }

        public Guid CompanyId { get; }
        public AddressInfo Address { get; }
    }
}