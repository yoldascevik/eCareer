using System.Threading.Tasks;
using Career.IoC;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Career.MediatR.Query;

public static class QueryExecutor
{
    public static async Task<TResult> Execute<TResult>(IQuery<TResult> query)
    {
        using (var scope = DIResolver.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(query);
        }
    }
}