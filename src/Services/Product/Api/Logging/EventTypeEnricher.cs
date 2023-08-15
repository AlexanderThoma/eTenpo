using System.Text;
using Murmur;
using Serilog.Core;
using Serilog.Events;

namespace eTenpo.Product.Api.Logging;

/// <summary>
/// This enricher adds the eventType property to the log entry which can be used to uniquely identify logs of the same type
/// </summary>
public class EventTypeEnricher : ILogEventEnricher
{
    private const string EventTypePropertyName = "EventType";

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent is null)
        {
            throw new ArgumentNullException(nameof(logEvent));
        }

        if (propertyFactory is null)
        {
            throw new ArgumentNullException(nameof(propertyFactory));
        }

        var murmurHash = MurmurHash.Create32();
        var bytes = Encoding.UTF8.GetBytes(logEvent.MessageTemplate.Text);
        var hash = murmurHash.ComputeHash(bytes);
        var hexadecimalHash = BitConverter.ToString(hash).Replace("-", "");
        
        var eventId = propertyFactory.CreateProperty(EventTypePropertyName, hexadecimalHash);
        logEvent.AddPropertyIfAbsent(eventId);
    }
}