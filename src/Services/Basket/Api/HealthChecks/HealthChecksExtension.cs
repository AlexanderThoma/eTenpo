using eTenpo.Basket.Api.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eTenpo.Basket.Api.HealthChecks;

public static class HealthChecksExtension
{
    private static readonly string[] RedisTags = ["startup"];

    public static IHealthChecksBuilder AddCustomHealthChecks(this IServiceCollection services, ConfigurationManager config)
    {
        return services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddRedis(config.GetRequiredConnectionString("Redis"), "redis", HealthStatus.Unhealthy, RedisTags);
            /*.AddAzureServiceBusTopic(
                _ => config.GetRequiredValue("EventBus:ConnectionString"),
                _ => config.GetRequiredValue("EventBus:TopicName"),
                name: "azureservicebus",
                tags: new[] { "startup" })*/
    }
}
