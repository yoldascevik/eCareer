using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Company.Domain.DomainEvents.Company;

public class CompanyCreatedEvent : DomainEvent
{
    private CompanyCreatedEvent(){} // for serialization
        
    public CompanyCreatedEvent(Entities.Company company)
    {
        Check.NotNull(company, nameof(company));
            
        CompanyId = company.Id;
        CompanyName = company.Name;
        Email = company.Email;
    }

    public Guid CompanyId { get; private set; }
    public string CompanyName { get; private set; }
    public string Email { get; private set; }
}