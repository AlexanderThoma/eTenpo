using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eTenpo.Product.Api.HealthChecks;

public class SampleHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        // TODO: health-check
        throw new NotImplementedException();
    }
}