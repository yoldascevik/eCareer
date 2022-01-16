using System;
using System.Linq;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Career.MediatR;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMediatRWithFluentValidation(this IServiceCollection services, params Type[] assemblyPointerTypes)
    {
        services.AddMediatR(assemblyPointerTypes);
        services.AddValidatorsFromAssembly(assemblyPointerTypes.First().Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}