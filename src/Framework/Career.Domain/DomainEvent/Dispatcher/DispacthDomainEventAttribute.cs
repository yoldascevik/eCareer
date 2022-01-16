using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore;
using AspectCore.Aspects;
using Career.Domain.Entities;
using Career.EventHub;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Career.Domain.DomainEvent.Dispatcher;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
public class DispacthDomainEventAttribute : AspectAttribute
{
    private IEventDispatcher _dispatcher;
    private ILogger<DispacthDomainEventAttribute> _logger;

    public override void OnSuccess(MethodExecutionArgs args)
    {
        IEnumerable<IDomainEvent> domainEvents = GetDomainEventsFromArgs(args.Arguments);
        if (domainEvents.Any())
        {
            _dispatcher.Dispatch(domainEvents);
            ClearAllDomainEventsFromArgs(args.Arguments);
                
            _logger.LogInformation("Domain events were dispatched and clear");
        }
    }

    public override async Task OnSuccessAsync(MethodExecutionArgs args)
    {
        IEnumerable<IDomainEvent> domainEvents = GetDomainEventsFromArgs(args.Arguments);
        if (domainEvents.Any())
        {
            await _dispatcher.Dispatch(domainEvents);
            ClearAllDomainEventsFromArgs(args.Arguments);
                
            _logger.LogInformation("Domain events were dispatched and clear");
        }
    }

    public override AspectAttribute LoadDependencies(IServiceProvider serviceProvider)
    {
        _dispatcher ??= serviceProvider.GetRequiredService<IEventDispatcher>();
        _logger ??= serviceProvider.GetRequiredService<ILogger<DispacthDomainEventAttribute>>();

        if (_dispatcher == null)
            throw new ArgumentException("IEventDispatcher is not registered on DI.");

        return base.LoadDependencies(serviceProvider);
    }

    private IEnumerable<IDomainEvent> GetDomainEventsFromArgs(object[] args)
    {
        var domainEventList = args?
            .Where(x => x is DomainEntity)
            .Cast<DomainEntity>()
            .SelectMany(e => e.DomainEvents)
            .ToList();
            
        return domainEventList ?? new List<IDomainEvent>();
    }
        
    private void ClearAllDomainEventsFromArgs(object[] args)
    {
        if (args != null && args.Any())
        {
            foreach (object argument in args)
            {
                if (argument is DomainEntity domainEntity)
                    domainEntity.ClearDomainEvents();
            }
        }
    }
}