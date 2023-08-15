using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eTenpo.Product.Api.Swagger;

public class ConfigureSwaggerOptions
    : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;

    public ConfigureSwaggerOptions(
        IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;
    }
    
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                CreateVersionInfo(description));
        }
    }
    
    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }
    
    private OpenApiInfo CreateVersionInfo(
        ApiVersionDescription desc)
    {
        var info = new OpenApiInfo
        {
            Title = "Product API",
            Version = desc.ApiVersion.ToString()
        };

        if (desc.IsDeprecated)
        {
            info.Description += " This API version has been deprecated. Please use a recent one.";
        }

        return info;
    }
}