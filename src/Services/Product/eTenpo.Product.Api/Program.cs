using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using CorrelationId.DependencyInjection;
using eTenpo.Product.Api.HealthChecks;
using eTenpo.Product.Api.Middleware;
using eTenpo.Product.Api.Services;
using eTenpo.Product.Api.Swagger;
using eTenpo.Product.Application;
using eTenpo.Product.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Polly;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

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

//builder.Services.AddCustomHealthChecks(builder.Configuration);

// TODO: use marker interfaces to register services 'on the fly', e.g. ITransient, IScoped, ISingleton, with "Scrutor"
// TODO: maybe it's worth to see decorator functionality of Scrutor for some services
// add dependencies from other layers
builder.Services
    .AddInfrastructure(builder.Configuration["ConnectionStrings:SqlServer"]!)
    .AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddQuartzServices();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

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

// TODO: add migration here
Policy retryPolicy = Policy.Handle<Exception>().WaitAndRetry(
    5,
    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
    (exception, timeSpan, context) => { Log.Error(exception, "Database is currently not available"); });

retryPolicy.Execute(
    () =>
    {
        using var dbContext = app.Services.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    });


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

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

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

/*app.MapHealthChecksUI(options =>
{
    options.ApiPath = "healthchecks-ui";
});*/

app.Run();