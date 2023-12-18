namespace NetCore.WebApiCommon.Infrastructure.Exceptions;

public class PropertyNotFoundException : ArgumentException
{
    public override string Message => CustomMessage ?? Message;
    private string? CustomMessage { get; set; }

    public PropertyNotFoundException(string customMessage)
    {
        CustomMessage = customMessage;
    }

    public PropertyNotFoundException()
    {
    }
}