using Microsoft.AspNetCore.Http;
using NetCore.Architecture.Api.Models;
using NetCore.Architecture.Core.Common.Enums;
using NetCore.Architecture.Core.Common.Helpers;

namespace NetCore.Architecture.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            LogHelper.WriteError(ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var response = new ApiResponse(false);
            response.Messages.Add(new AppMessage(ex.Message, AppMessageType.Error));

            await context.Response.WriteAsync(response.ToString() ?? string.Empty);
        }
    }
}