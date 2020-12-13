using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Company.Domain.DomainEvents.Company
{
    public class CompanyDeletedEvent : DomainEvent
    {
        public CompanyDeletedEvent(Entities.Company company)
        {
            Check.NotNull(company, nameof(company));

            CompanyId = company.Id;
            CompanyName = company.Name;
            CompanyEmail = company.Email;
        }
        
        public Guid CompanyId { get; }
        public string CompanyName { get; }
        public string CompanyEmail { get; }
    }
}