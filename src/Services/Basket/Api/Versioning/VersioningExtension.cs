using Asp.Versioning;

namespace eTenpo.Basket.Api.Versioning;

public static class VersioningExtension
{
    public static IApiVersioningBuilder AddRouteApiVersioning(this IServiceCollection services)
    {
        return services.AddApiVersioning(options =>
            {
                // show api versions in response header (api-supported-versions)
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            })
            .AddApiExplorer(options =>
            {
                // "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });
    }
}
