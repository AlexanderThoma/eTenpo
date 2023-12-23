using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using eTenpo.Basket.Api.Endpoints;
using eTenpo.Basket.Api.Logging;
using eTenpo.Basket.Api.Services;
using eTenpo.Basket.Api.Versioning;
using FluentValidation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, configuration) =>

    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithEventTypeEnricher()
);

builder.Services.AddRouteApiVersioning();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "master";
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

app.UseHttpsRedirection();

app.MapEndpoints(versionSet);

app.Run();
