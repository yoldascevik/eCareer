using System;
using Career.EventHub;

namespace Job.Application.Job.IntegrationEvents;

public class CompanyNameUpdatedEvent: IntegrationEvent
{
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }
}