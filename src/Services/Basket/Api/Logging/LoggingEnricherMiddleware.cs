using Serilog.Context;

namespace eTenpo.Basket.Api.Logging;

// TODO: put into nuget package for better distributability

/// <summary>
/// This middleware enriches the log context with new properties for better debugging
/// </summary>
public class LoggingEnricherMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);
        
        var userName = context.User.Identity is { IsAuthenticated: true } ? context.User.Identity.Name : "anonymous";
        LogContext.PushProperty("User", userName);

        var ipAddress = context.Connection.RemoteIpAddress is null ? "unknown" : context.Connection.RemoteIpAddress.ToString();
        LogContext.PushProperty("IpAddress", ipAddress);

        if (context.Request.RouteValues.TryGetValue("version", out var apiVersion))
        {
            var version = apiVersion?.ToString();
            LogContext.PushProperty("ApiVersion", version);
        }

        var userAgent = context.Request.Headers["User-Agent"].FirstOrDefault();
        LogContext.PushProperty("UserAgent", userAgent);

        // TODO: how to version app/container correctly???
        //var appVersion = context.Connection.RemoteIpAddress is null ? "unknown" : context.Connection.RemoteIpAddress.ToString();
        //LogContext.PushProperty("IpAddress", ipAddress);

        await next.Invoke(context);
    }
}
