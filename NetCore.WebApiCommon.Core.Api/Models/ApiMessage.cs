using NetCore.WebApiCommon.Core.Common.Enums;

namespace NetCore.WebApiCommon.Api.Models;

public class ApiMessage
{
    public string Content { get; set; }
    public ApiMessageType Type { get; set; }

    public ApiMessage()
    {
        Content = string.Empty;
    }
    
    public ApiMessage(string content, ApiMessageType messageType = ApiMessageType.Info)
    {
        Content = content;
        Type = messageType;
    }
}