using eTenpo.Product.Infrastructure;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eTenpo.Product.Api.HealthChecks;

// samples for kubernetes https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks/blob/master/doc/k8s-operator.md
// explanation of health checks https://andrewlock.net/deploying-asp-net-core-applications-to-kubernetes-part-6-adding-health-checks-with-liveness-readiness-and-startup-probes/

public static class HealthChecksExtension
{
    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddDbContextCheck<ApplicationDbContext>(name: "sqlserver", HealthStatus.Unhealthy,
                tags: new[] { "startup" })
            /*.AddAzureServiceBusTopic(
                _ => config.GetRequiredValue("EventBus:ConnectionString"),
                _ => config.GetRequiredValue("EventBus:TopicName"),
                name: "azureservicebus",
                tags: new[] { "startup" })*/;
        
        return services;
    }
}