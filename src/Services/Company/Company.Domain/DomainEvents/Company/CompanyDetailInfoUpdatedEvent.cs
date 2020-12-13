using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Company.Domain.DomainEvents.Company
{
    public class CompanyDetailInfoUpdatedEvent : DomainEvent, IDomainEvent
    {
        public CompanyDetailInfoUpdatedEvent(Entities.Company company)
        {
            Check.NotNull(company,nameof(company));

            company = Company;
        }

        public Entities.Company Company { get; set; }
    }
}