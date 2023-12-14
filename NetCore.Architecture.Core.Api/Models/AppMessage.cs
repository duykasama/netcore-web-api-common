using NetCore.Architecture.Core.Common.Enums;

namespace NetCore.Architecture.Api.Models;

public class AppMessage
{
    public string Content { get; set; }
    public AppMessageType Type { get; set; }
}