using System;
using MediatR;

namespace Career.Domain.DomainEvent
{
    public interface IDomainEvent: INotification
    {
        Guid EventId { get; }
        DateTime OccurredOn { get; }
    }
}