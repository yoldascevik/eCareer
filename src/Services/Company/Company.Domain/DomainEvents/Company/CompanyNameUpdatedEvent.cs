using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Company.Domain.DomainEvents.Company
{
    public class CompanyNameUpdatedEvent : DomainEvent
    {
        public CompanyNameUpdatedEvent(Entities.Company company, string oldCompanyName)
        {
            Check.NotNull(company, nameof(company));
            Check.NotNullOrEmpty(oldCompanyName, nameof(oldCompanyName));
            
            CompanyId = company.Id;
            CompanyName = company.Name;
            OldCompanyName = oldCompanyName;
        }

        public Guid CompanyId { get; }
        public string CompanyName { get;}
        public string OldCompanyName { get;}
    }
}