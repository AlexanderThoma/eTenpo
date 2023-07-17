using eTenpo.Product.Api.Configuration;
using eTenpo.Product.Infrastructure;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eTenpo.Product.Api.HealthChecks;

// samples for kubernetes https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks/blob/master/doc/k8s-operator.md
// explanation of health checks https://andrewlock.net/deploying-asp-net-core-applications-to-kubernetes-part-6-adding-health-checks-with-liveness-readiness-and-startup-probes/

public static class HealthChecksExtension
{
    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager config)
    {
        var sqlConnectionString = config.GetRequiredConnectionString("SqlServer");

        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddDbContextCheck<ApplicationDbContext>(name: "database", HealthStatus.Degraded,
                tags: new[] { "startup" });
        //.AddSqlServer(sqlConnectionString, name: "sqlserver", tags: new[] { "startup" })
            /*.AddAzureServiceBusTopic(
                _ => config.GetRequiredValue("EventBus:ConnectionString"),
                _ => config.GetRequiredValue("EventBus:TopicName"),
                name: "azureservicebus",
                tags: new[] { "startup" });*/
        
        /*services.AddHealthChecksUI(setup =>
        {
            //All the excedent requests will result in 429 (Too many requests)
            setup.SetApiMaxActiveRequests(3);
            
            // UI polls every 10 seconds for status
            setup.SetEvaluationTimeInSeconds(10);

            // execution history of an endpoint is saved up to 50 entries
            setup.MaximumHistoryEntriesPerEndpoint(50);

        })
            //.AddSqlServerStorage(sqlConnectionString)
            ;
        */
        return services;
    }
}