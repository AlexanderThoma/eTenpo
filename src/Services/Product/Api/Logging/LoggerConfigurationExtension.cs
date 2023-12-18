using Serilog;
using Serilog.Configuration;

namespace eTenpo.Product.Api.Logging;

public static class LoggerConfigurationExtension
{
    public static LoggerConfiguration WithEventTypeEnricher(
        this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        return enrichmentConfiguration != null ? enrichmentConfiguration.With<EventTypeEnricher>() : throw new ArgumentNullException(nameof (enrichmentConfiguration));
    }
}