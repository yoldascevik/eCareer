using System.Threading.Tasks;
using Career.Exceptions;
using Career.IoC;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Career.MediatR.Notification;

public static class NotificationPublisher
{
    public static async Task Publish(INotification notification)
    {
        Check.NotNull(notification, nameof(notification));
            
        using var scope = DIResolver.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            
        await mediator.Publish(notification);
    }
}