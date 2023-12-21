using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetCore.WebApiCommon.Core.Common.Constants;
using NetCore.WebApiCommon.Core.Constants;
using NetCore.WebApiCommon.Core.Settings;
using NetCore.WebApiCommon.Infrastructure.Exceptions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetCore.WebApiCommon.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    private static IConfiguration Configuration { get; set; }

    public static void InitConfiguration(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services, SwaggerSettings? swaggerSettings = default)
    {
        swaggerSettings ??= Configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>() ?? throw new MissingSwaggerSettingsException();
        
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

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        var jwtSettings = Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>() ?? throw new MissingJwtSettingsException();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtSettings.Issuer,
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidAudience = jwtSettings.Audience,
                ValidateAudience = jwtSettings.ValidateAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey)),
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                ValidateLifetime = jwtSettings.ValidateLifetime,
                ClockSkew = TimeSpan.Zero
            };
        });
        
        return services;
    }

    public static IServiceCollection AddDefaultCorsPolicy(this IServiceCollection services)
    {
        var corsSettings = Configuration.GetSection(nameof(CorsSettings)).Get<CorsSettings>() ??
                           throw new MissingCorsSettingsException();
        services.AddCors(options =>
        {
            options.AddPolicy(CorsConstants.APP_CORS_POLICY, builder =>
            {
                builder.WithOrigins(corsSettings.GetAllowedOriginsArray())
                    .WithHeaders(corsSettings.GetAllowedHeadersArray())
                    .WithMethods(corsSettings.GetAllowedMethodsArray());
                if (corsSettings.AllowCredentials)
                {
                    builder.AllowCredentials();
                }

                builder.Build();
            });
        });
        
        return services;
    }
}