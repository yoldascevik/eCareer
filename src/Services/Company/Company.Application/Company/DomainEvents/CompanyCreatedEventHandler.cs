using System.Threading.Tasks;
using Career.CAP.DomainEvent;
using Company.Domain.DomainEvents.Company;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.DomainEvents
{
    public class CompanyCreatedEventHandler : CAPDomainEventHandler<CompanyCreatedEvent>
    {
        private readonly ILogger<CompanyCreatedEventHandler> _logger;

        public CompanyCreatedEventHandler(ILogger<CompanyCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        [CapSubscribe(nameof(CompanyCreatedEvent))]
        public override async Task Handle(CompanyCreatedEvent domainEvent)
        {
            _logger.LogDebug("{Event} is handled! {CompanyId} / {CompanyName} / {Email}",
                domainEvent.GetType().Name, domainEvent.CompanyId, domainEvent.CompanyName, domainEvent.Email);

            await Task.CompletedTask;
        }
    }
}