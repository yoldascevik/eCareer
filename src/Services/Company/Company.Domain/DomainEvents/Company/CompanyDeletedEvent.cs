using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Company.Domain.DomainEvents.Company;

public class CompanyDeletedEvent : DomainEvent
{
    private CompanyDeletedEvent(){} // for serialization

    public CompanyDeletedEvent(Entities.Company company)
    {
        Check.NotNull(company, nameof(company));

        CompanyId = company.Id;
        CompanyName = company.Name;
        CompanyEmail = company.Email;
    }
        
    public Guid CompanyId { get; private set; }
    public string CompanyName { get; private set; }
    public string CompanyEmail { get; private set; }
}