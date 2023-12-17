﻿namespace NetCore.WebApiCommon.Infrastructure.Api.Exceptions;

public class MissingSwaggerSettingsException : ArgumentNullException
{
    public override string Message => CustomMessage ?? Message;
    private string? CustomMessage { get; set; }

    public MissingSwaggerSettingsException(string customMessage)
    {
        CustomMessage = customMessage;
    }

    public MissingSwaggerSettingsException()
    {
    }
}