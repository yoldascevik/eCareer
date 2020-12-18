using System.Threading;
using System.Threading.Tasks;
using Career.Domain.DomainEvent;
using Company.Domain.DomainEvents.Company;
using Microsoft.Extensions.Logging;

namespace Company.Application.Company.DomainEvents
{
    public class CompanyCreatedEventHandler : IDomainEventHandler<CompanyCreatedEvent>
    {
        private readonly ILogger<CompanyCreatedEventHandler> _logger;

        public CompanyCreatedEventHandler(ILogger<CompanyCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(CompanyCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("{Event} is handled! {CompanyId} / {CompanyName} / {Email}",
                notification.GetType().Name, notification.CompanyId, notification.CompanyName, notification.Email);

            await Task.CompletedTask;
        }
    }
}