using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Career.MediatR
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMediatRPipelineBehavior(this IServiceCollection services) 
            => services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}