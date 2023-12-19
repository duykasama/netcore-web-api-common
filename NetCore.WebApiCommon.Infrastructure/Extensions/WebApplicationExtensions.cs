using Microsoft.AspNetCore.Builder;
using NetCore.WebApiCommon.Core.Common.Models;

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
}