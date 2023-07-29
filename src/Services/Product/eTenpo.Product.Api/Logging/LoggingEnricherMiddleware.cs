using Serilog.Context;

namespace eTenpo.Product.Api.Logging;

/// <summary>
/// This middleware enriches the log context with new properties for better debugging
/// </summary>
public class LoggingEnricherMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var userName = context.User.Identity is { IsAuthenticated: true } ? context.User.Identity.Name : "anonymous";
        LogContext.PushProperty("User", userName);

        var ipAddress = context.Connection.RemoteIpAddress is null ? "unknown" : context.Connection.RemoteIpAddress.ToString();
        LogContext.PushProperty("IpAddress", ipAddress);

        if (context.Request.RouteValues.TryGetValue("version", out var apiVersion))
        {
            var version = apiVersion?.ToString();
            LogContext.PushProperty("ApiVersion", version);
        }

        // TODO: how to version app/container correctly???
        //var appVersion = context.Connection.RemoteIpAddress is null ? "unknown" : context.Connection.RemoteIpAddress.ToString();
        //LogContext.PushProperty("IpAddress", ipAddress);
        
        await next.Invoke(context);
    }
}