using Asp.Versioning;

namespace Customer.WebApi.Extensions;

public static class ApiVersionExtension
{
    public static IServiceCollection AddApiVersionService(this IServiceCollection service)
    {
        service.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new(1);
            config.ReportApiVersions = true;
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ApiVersionReader = new HeaderApiVersionReader("X-API-VERSION");

        }).AddApiExplorer(config =>
        {
            config.GroupNameFormat = "'v'VVV";
            config.SubstituteApiVersionInUrl = true;
        });

        return service;
    }
}