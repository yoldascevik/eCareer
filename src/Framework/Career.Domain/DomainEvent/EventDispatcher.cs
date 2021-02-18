using System.Collections.Generic;
using System.Threading.Tasks;
using Career.Exceptions;
using Career.IoC;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Domain.DomainEvent
{
    public static class EventDispatcher
    {
        public static async Task Dispatch(IDomainEvent domainEvent)
        {
            Check.NotNull(domainEvent, nameof(domainEvent));
            
            using var scope = DIResolver.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            
            await mediator.Publish(domainEvent);
        }
        
        public static async Task Dispatch(IEnumerable<IDomainEvent> domainEvents)
        {
            Check.NotNull(domainEvents, nameof(domainEvents));

            foreach (IDomainEvent domainEvent in domainEvents)
            {
                await Dispatch(domainEvent);
            }
        }
    }
}