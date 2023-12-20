using Microsoft.AspNetCore.Http;
using NetCore.WebApiCommon.Api.Models;
using NetCore.WebApiCommon.Core.Common.Enums;
using NetCore.WebApiCommon.Core.Common.Helpers;

namespace NetCore.WebApiCommon.Api.Middlewares;

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
            response.Messages.Add(new ApiMessage(ex.Message, ApiMessageType.Error));

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}