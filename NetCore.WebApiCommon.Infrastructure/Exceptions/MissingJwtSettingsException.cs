namespace NetCore.WebApiCommon.Infrastructure.Exceptions;

public class MissingJwtSettingsException : ArgumentNullException
{
    public override string Message => CustomMessage ?? Message;
    private string? CustomMessage { get; set; }

    public MissingJwtSettingsException(string customMessage)
    {
        CustomMessage = customMessage;
    }

    public MissingJwtSettingsException()
    {
    }
}