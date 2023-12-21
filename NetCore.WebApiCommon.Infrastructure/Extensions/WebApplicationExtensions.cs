using Microsoft.AspNetCore.Builder;
using NetCore.WebApiCommon.Core.Common.Models;
using NetCore.WebApiCommon.Core.Constants;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace NetCore.WebApiCommon.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
    public static void UseSwaggerInEnvironments(this WebApplication app, params string[] environments)
    {
        if (!environments.Contains(GlobalData.CurrentEnvironment)) return;
     
        UseSwaggerInEnvironmentInternal(app);
    }
    
    public static void UseSwaggerInEnvironments(this IApplicationBuilder app, params string[] environments)
    {
        if (!environments.Contains(GlobalData.CurrentEnvironment)) return;
        
        UseSwaggerInEnvironmentInternal(app);
    }

    public static void UseAppCors(this WebApplication app)
    {
        app.UseCors(CorsConstants.APP_CORS_POLICY);
    }
    
    public static void UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors(CorsConstants.APP_CORS_POLICY);
    }

    private static void UseSwaggerInEnvironmentInternal(dynamic app)
    {
        Action<SwaggerUIOptions> action = options =>
        {
            options.DisplayRequestDuration();
            options.EnableTryItOutByDefault();
        };
        
        app.UseSwagger();
        app.UseSwaggerUI(action);
    }
}