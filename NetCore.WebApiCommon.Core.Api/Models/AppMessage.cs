using NetCore.WebApiCommon.Core.Common.Enums;

namespace NetCore.WebApiCommon.Api.Models;

public class AppMessage
{
    public string Content { get; set; }
    public AppMessageType Type { get; set; }

    public AppMessage()
    {
        Content = string.Empty;
    }
    
    public AppMessage(string content, AppMessageType messageType = AppMessageType.Info)
    {
        Content = content;
        Type = messageType;
    }
}