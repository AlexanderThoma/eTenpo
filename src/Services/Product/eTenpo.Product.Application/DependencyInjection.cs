using eTenpo.Product.Application.PipelineBehaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace eTenpo.Product.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var appAssembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(config =>
        {
            // only registers handlers
            config.RegisterServicesFromAssembly(appAssembly);
            
            // add pipeline behavior separately, !! order represents execution order in pipeline !!
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>), ServiceLifetime.Scoped);
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), ServiceLifetime.Transient);
        });
        
        // add fluentValidation validators
        services.AddValidatorsFromAssembly(appAssembly);
        
        return services;
    }
}