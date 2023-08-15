using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using CorrelationId;
using CorrelationId.DependencyInjection;
using eTenpo.Product.Api.HealthChecks;
using eTenpo.Product.Api.Logging;
using eTenpo.Product.Api.Middleware;
using eTenpo.Product.Api.Services;
using eTenpo.Product.Api.Swagger;
using eTenpo.Product.Application;
using eTenpo.Product.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// TODO: evaluate better way of logging centrally providing an event id for the log

builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, configuration) =>

    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithEventTypeEnricher()
);

builder.Services.AddScoped<LoggingEnricherMiddleware>();

// can be used for debugging logging issues with the config
//Serilog.Debugging.SelfLog.Enable(Console.Error);

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

builder.Services.AddTransient<GlobalExceptionMiddleware>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<EventTypeEnricher>();

builder.Services.AddCustomHealthChecks(builder.Configuration);

// TODO: addOpenTelemetry with logging, tracing, metrics (with azure monitor export)

// TODO: use marker interfaces to register services 'on the fly', e.g. ITransient, IScoped, ISingleton, with "Scrutor"
// TODO: maybe it's worth to see decorator functionality of Scrutor for some services
// add dependencies from other layers
builder.Services
    .AddInfrastructure(builder.Configuration["ConnectionStrings:SqlServer"]!)
    .AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddQuartzServices();

builder.Services.AddApiVersioning(options =>
{
    // show api versions in response header (api-supported-versions)
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
}).AddApiExplorer(options =>
{
    // "'v'major[.minor][-status]"
    options.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();
    
MigrationService.Migrate(app);

app.UseMiddleware<GlobalExceptionMiddleware>();

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
    });
}

app.UseSerilogRequestLogging(options =>
{
    // Sets the properties only in the http request
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("HttpMethod", httpContext.Request.Method);
        diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());
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

//app.UseAuthentication();
//app.UseAuthorization();
//app.UseSession();

app.MapControllers();

app.UseCorrelationId();

app.UseMiddleware<LoggingEnricherMiddleware>();

// TODO: security, add requireCors and requireAuthorization, consider deactivation of caching

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

app.Run();