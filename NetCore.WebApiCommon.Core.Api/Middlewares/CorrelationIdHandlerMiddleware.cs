﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NetCore.WebApiCommon.Core.Common.Constants;
using NetCore.WebApiCommon.Core.Common.Interfaces;

namespace NetCore.WebApiCommon.Api.Middlewares;

public class CorrelationIdHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
    {
        var correlationId = GetCorrelationId(context, correlationIdGenerator);
        context.Response.Headers.TryGetValue(CoreConstants.CORRELATION_ID_HEADER, out var id);
        if (string.IsNullOrEmpty(id))
        {
            context.Response.Headers[CoreConstants.CORRELATION_ID_HEADER] = correlationId;
        }
        await _next(context);
    }

    private static StringValues GetCorrelationId(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
    {
        if (context.Request.Headers.TryGetValue(CoreConstants.CORRELATION_ID_HEADER, out var correlationId))
        {
            correlationIdGenerator.Set(correlationId);
            return correlationId;
        }

        return correlationIdGenerator.Get();
    }
}