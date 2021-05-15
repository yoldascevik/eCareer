using System.Threading.Tasks;
using Career.CAP.IntegrationEvent;
using DotNetCore.CAP;
using Job.Application.Job.IntegrationEvents;
using Job.Domain.JobAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.EventHandlers.Integration
{
    public class CompanyNameUpdatedEventHandler : CAPIntegrationEventHandler<CompanyNameUpdatedEvent>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<CompanyNameUpdatedEventHandler> _logger;

        public CompanyNameUpdatedEventHandler(IJobRepository jobRepository, ILogger<CompanyNameUpdatedEventHandler> logger)
        {
            _jobRepository = jobRepository;
            _logger = logger;
        }

        [CapSubscribe(nameof(CompanyNameUpdatedEvent))]
        public override async Task Handle(CompanyNameUpdatedEvent @event)
        {
            await _jobRepository.UpdateCompanyNamesInJob(@event.CompanyId, @event.CompanyName);
            
            _logger.LogInformation("Company name updated in jobs. Company : {CompanyId} - {CompanyName}", @event.CompanyId, @event.CompanyName);
        }
    }
}