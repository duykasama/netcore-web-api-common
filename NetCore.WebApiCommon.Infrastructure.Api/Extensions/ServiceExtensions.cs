using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NetCore.WebApiCommon.Core.Common.Helpers;
using NetCore.WebApiCommon.Core.Settings;
using NetCore.WebApiCommon.Infrastructure.Api.Exceptions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetCore.WebApiCommon.Infrastructure.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services, SwaggerSettings? swaggerSettings = default)
    {
        var configuration = DependencyInjectionHelper.ResolveService<IConfiguration>();
        swaggerSettings ??= configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>() ?? throw new MissingSwaggerSettingsException();
        
        services.AddSwaggerGen(
            options => 
            { 
                options.SwaggerDoc(swaggerSettings.Version, new OpenApiInfo
                {
                    Version = swaggerSettings.Version,
                    Title = swaggerSettings.Title,
                    Description = swaggerSettings.Description,
                    TermsOfService = swaggerSettings.GetTermsOfService(),
                    Contact = swaggerSettings.GetContact(),
                    License = swaggerSettings.GetLicense()
                });
                options.SwaggerGeneratorOptions = new SwaggerGeneratorOptions()
                {
                    Servers = swaggerSettings.GetServers()
                };
                options.AddSecurityDefinition(swaggerSettings.Options.SecurityScheme.Name, swaggerSettings.GetSecurityScheme());
                options.AddSecurityRequirement(swaggerSettings.GetSecurityRequirement());
            }
        );
        
        return services;
    }

    public static void UseSwaggerInEnvironments(this WebApplication app, params string[] environments)
    {
        if (!environments.Contains(app.Environment.EnvironmentName)) return;
        
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DisplayRequestDuration();
            options.EnableTryItOutByDefault();
        });
    }
}