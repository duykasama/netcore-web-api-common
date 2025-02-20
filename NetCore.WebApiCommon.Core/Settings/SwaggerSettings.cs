using Microsoft.OpenApi.Models;
using NetCore.WebApiCommon.Core.Common.Helpers;

namespace NetCore.WebApiCommon.Core.Settings;

public class SwaggerSettings
{
    public string Version { get; init; } = default!;
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string TermsOfServiceUrl { get; init; } = default!;
    public string ContactName { get; init; } = default!;
    public string ContactEmail { get; init; } = default!;
    public string ContactUrl { get; init; } = default!;
    public string LicenseName { get; init; } = default!;
    public string LicenseUrl { get; init; } = default!;
    public SwaggerOptions Options { get; init; } = new();

    public OpenApiContact GetContact()
    {
        return new OpenApiContact
        {
            Name = ContactName,
            Url = new Uri(ContactUrl),
            Email = ContactEmail
        };
    }

    public OpenApiLicense GetLicense()
    {
        return new OpenApiLicense
        {
            Name = LicenseName,
            Url = new Uri(LicenseUrl)
        };
    }

    public Uri GetTermsOfService()
    {
        return new Uri(TermsOfServiceUrl);
    }

    public List<OpenApiServer> GetServers()
    {
        return [.. Options.Servers.Where(s => !string.IsNullOrEmpty(s.Url)).Select(s => new OpenApiServer
        {
            Url = s.Url,
            Description = s.Description,
            Variables = s.Variables.Count == 0
                ? s.Variables.ToDictionary(
                    v => v.Name,
                    v => new OpenApiServerVariable()
                    {
                        Description = v.Description,
                        Default = v.DefaultValue
                    })
                : []

        })];
    }

    public OpenApiSecurityScheme GetSecurityScheme()
    {
        var securityScheme = Options.SecurityScheme;
        return new OpenApiSecurityScheme
        {
            Name = securityScheme.Name,
            Description = securityScheme.Description,
            Type = securityScheme.GetSecuritySchemeType(),
            In = securityScheme.GetParameterLocation()
        };
    }

    public OpenApiSecurityRequirement GetSecurityRequirement()
    {
        var securityRequirement = Options.SecurityRequirement;
        return new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = securityRequirement.GetReferenceType(),
                        Id = securityRequirement.Id
                    }
                },
                Array.Empty<string>()
            }
        };
    }
}

public class SwaggerOptions
{
    public List<SwaggerServer> Servers { get; init; } = [];
    public SwaggerSecurityScheme SecurityScheme { get; init; } = default!;
    public SwaggerSecurityRequirement SecurityRequirement { get; init; } = default!;
}

public class SwaggerServer
{
    public string Url { get; init; } = default!;
    public string Description { get; init; } = default!;
    public List<SwaggerServerVariable> Variables { get; init; } = new();
}

public class SwaggerServerVariable
{
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string DefaultValue { get; init; } = default!;
}

public class SwaggerSecurityScheme
{
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string Type { get; init; } = default!;
    public string Location { get; init; } = default!;

    public SecuritySchemeType GetSecuritySchemeType()
    {
        return EnumHelper.GetEnumValueFromString<SecuritySchemeType>(Type);
    }

    public ParameterLocation GetParameterLocation()
    {
        return EnumHelper.GetEnumValueFromString<ParameterLocation>(Location);
    }
}

public class SwaggerSecurityRequirement
{
    public string Type { get; init; } = default!;
    public string Id { get; init; } = default!;

    public ReferenceType GetReferenceType()
    {
        return EnumHelper.GetEnumValueFromString<ReferenceType>(Type);
    }
}