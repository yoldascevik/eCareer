using System;
using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Company.Domain.DomainEvents.Company
{
    public class CompanyNameUpdatedEvent : DomainEvent
    {
        private CompanyNameUpdatedEvent(){} // for serialization

        public CompanyNameUpdatedEvent(Entities.Company company, string oldCompanyName)
        {
            Check.NotNull(company, nameof(company));
            Check.NotNullOrEmpty(oldCompanyName, nameof(oldCompanyName));
            
            CompanyId = company.Id;
            CompanyName = company.Name;
            OldCompanyName = oldCompanyName;
        }

        public Guid CompanyId { get; private set; }
        public string CompanyName { get; private set; }
        public string OldCompanyName { get; private set; }
    }
}