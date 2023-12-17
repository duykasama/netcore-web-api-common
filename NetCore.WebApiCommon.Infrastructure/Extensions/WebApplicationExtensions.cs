using Microsoft.AspNetCore.Builder;

namespace NetCore.WebApiCommon.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
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