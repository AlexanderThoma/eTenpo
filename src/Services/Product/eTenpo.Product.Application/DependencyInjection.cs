using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eTenpo.Product.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var appAssembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(appAssembly);
        });

        services.AddValidatorsFromAssembly(appAssembly);
        
        return services;
    }
}