using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;
using Company.Domain.Values;

namespace Company.Domain.DomainEvents.Company
{
    public class CompanyTaxInfoUpdatedEvent : DomainEvent, IDomainEvent
    {
        public CompanyTaxInfoUpdatedEvent(Entities.Company company)
        {
            Check.NotNull(company, nameof(company));

            CompanyId = company.Id;
            TaxInfo = company.TaxInfo;
        }

        public Guid CompanyId { get; }
        public TaxInfo TaxInfo { get; }
    }
}