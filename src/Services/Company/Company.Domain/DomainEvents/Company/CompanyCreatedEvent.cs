using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Company.Domain.DomainEvents.Company
{
    public class CompanyCreatedEvent : DomainEvent
    {
        public CompanyCreatedEvent(Entities.Company company)
        {
            Check.NotNull(company, nameof(company));
            
            CompanyId = company.Id;
            CompanyName = company.Name;
            Email = company.Email;
        }

        public Guid CompanyId { get; }
        public string CompanyName { get; }
        public string Email { get; }
    }
}