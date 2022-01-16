using Career.Domain.DomainEvent;
using Career.Exceptions;
using Company.Domain.ValueObjects;

namespace Company.Domain.DomainEvents.Company;

public class CompanyTaxInfoUpdatedEvent : DomainEvent
{
    private CompanyTaxInfoUpdatedEvent(){} // for serialization

    public CompanyTaxInfoUpdatedEvent(Entities.Company company)
    {
        Check.NotNull(company, nameof(company));

        CompanyId = company.Id;
        TaxInfo = company.TaxInfo;
    }

    public Guid CompanyId { get; private set; }
    public TaxInfo TaxInfo { get; private set; }
}