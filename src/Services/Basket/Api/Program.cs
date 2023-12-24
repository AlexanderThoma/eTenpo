using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using CorrelationId;
using CorrelationId.DependencyInjection;
using eTenpo.Basket.Api.Endpoints;
using eTenpo.Basket.Api.HealthChecks;
using eTenpo.Basket.Api.Logging;
using eTenpo.Basket.Api.Services;
using eTenpo.Basket.Api.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, configuration) =>

    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithEventTypeEnricher()
);

builder.Services.AddCustomHealthChecks(builder.Configuration);

builder.Services.AddCors();

builder.Services.AddHttpContextAccessor();

builder.Services.AddRouteApiVersioning();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "master";
});

// setup correlation id to be set, if not existing
builder.Services.AddDefaultCorrelationId(options =>
{
    options.AddToLoggingScope = true;
    options.EnforceHeader = false;
    options.IgnoreRequestHeader = false;
    options.IncludeInResponse = true;
    options.RequestHeader = "X-Correlation-Id";
    options.ResponseHeader = "X-Correlation-Id";
    options.UpdateTraceIdentifier = false;
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var versionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 0))
    .ReportApiVersions()
    .Build();

// TODO: implement properly
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
        
        options.DisplayRequestDuration();
        options.RoutePrefix = string.Empty;
    });
}

app.UseSerilogRequestLogging(options =>
{
    // Sets the properties only in the http request
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("HttpMethod", httpContext.Request.Method);
        diagnosticContext.Set("UserAgent", httpContext.Request.Headers.UserAgent.FirstOrDefault());
        diagnosticContext.Set("QueryString",
            httpContext.Request.QueryString.HasValue ? httpContext.Request.QueryString.Value : string.Empty);
        diagnosticContext.Set("StatusCode", httpContext.Response.StatusCode);

    };
    
    options.MessageTemplate = 
        "HTTP {RequestMethod} {RequestPath}{QueryString} responded {StatusCode} in {Elapsed:0.0000} ms";
    
    options.GetLevel = (ctx, elapsed, ex) =>
    {
        if (ex != null || ctx.Response.StatusCode > 499)
        {
            return LogEventLevel.Error;
        }
        
        return ctx.Response.StatusCode > 399 ? LogEventLevel.Warning : LogEventLevel.Information;
    };
});

app.UseCorrelationId();

// health check for dependencies, 
app.MapHealthChecks("/healthz/startup", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("startup")
});

// check if app is alive, therefore simple return of true is sufficient
// if not alive -> Kubernetes will recreate the pod
app.MapHealthChecks("/healthz/liveness", new HealthCheckOptions
{
    // reference health check by name
    Predicate = r => r.Name.Contains("self")
});

// if fail -> Kubernetes will leave pod running but won't send traffic
// actually unnecessary, because same as liveness-check (here for completeness)
app.MapHealthChecks("/healthz/readiness", new HealthCheckOptions
{
    // reference health check by name
    Predicate = r => r.Name.Contains("self")
});

app.MapEndpoints(versionSet);

app.Run();
