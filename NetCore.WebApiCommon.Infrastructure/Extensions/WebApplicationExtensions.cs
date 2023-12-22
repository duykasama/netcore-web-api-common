using Microsoft.AspNetCore.Builder;
using NetCore.WebApiCommon.Core.Common.Models;
using NetCore.WebApiCommon.Core.Constants;

namespace NetCore.WebApiCommon.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
    public static void UseSwaggerInEnvironments(this WebApplication app, params string[] environments)
    {
        if (!environments.Contains(GlobalData.CurrentEnvironment)) return;
        
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DisplayRequestDuration();
            options.EnableTryItOutByDefault();
        });
    }
    
    public static void UseSwaggerInEnvironments(this IApplicationBuilder app, params string[] environments)
    {
        if (!environments.Contains(GlobalData.CurrentEnvironment)) return;
        
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DisplayRequestDuration();
            options.EnableTryItOutByDefault();
        });
    }

    public static void UseDefaultCorsPolicy(this WebApplication app)
    {
        app.UseCors(CorsConstants.APP_CORS_POLICY);
    }
    
    public static void UseDefaultCorsPolicy(this IApplicationBuilder app)
    {
        app.UseCors(CorsConstants.APP_CORS_POLICY);
    }
}