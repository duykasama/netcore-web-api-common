namespace NetCore.WebApiCommon.Infrastructure.Exceptions;

public class MissingCorsSettingsException : ArgumentException
{
    public MissingCorsSettingsException()
    {
    }

    public MissingCorsSettingsException(string message) : base(message)
    {
    }
}