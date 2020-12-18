using System.Threading.Tasks;
using Career.IoC;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Career.MediatR.Command
{
    public static class CommandExecutor
    {
        public static async Task Execute(ICommand command)
        {
            using (var scope = DIResolver.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(command);
            }
        }

        public static async Task<TResult> Execute<TResult>(ICommand<TResult> command)
        {
            using (var scope = DIResolver.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return await mediator.Send(command);
            }
        }
    }
}