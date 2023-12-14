using NetCore.Architecture.Core.Common.Enums;

namespace NetCore.Architecture.Api.Models;

public class AppApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public T Data { get; set; }
    public IList<AppMessage> Messages { get; set; }
    public AppApiResponse(bool isSuccess)
    {
        IsSuccess = isSuccess;
        Messages = new List<AppMessage>();
    }

    public AppApiResponse() : this(false)
    {
    }

    public AppApiResponse<T> AddMessage(string messageContent, AppMessageType type)
    {
        Messages.Add(new AppMessage() {Content = messageContent, Type = type});
        return this;
    }

    public AppApiResponse<T> AddSuccessMessage(string messageContent)
    {
        Messages.Add(new AppMessage(){ Content = messageContent, Type = AppMessageType.Success});
        return this;
    }
    
    public AppApiResponse<T> AddWarningMessage(string messageContent)
    {
        Messages.Add(new AppMessage(){ Content = messageContent, Type = AppMessageType.Warning});
        return this;
    }
    
    public AppApiResponse<T> AddErrorMessage(string messageContent)
    {
        Messages.Add(new AppMessage(){ Content = messageContent, Type = AppMessageType.Error});
        return this;
    }
}

public class AppApiResponse : AppApiResponse<object>
{
    public AppApiResponse(bool isSuccess): base(isSuccess)
    {
    }

    public AppApiResponse() : this(false)
    {
    }

    public AppApiResponse(object data) : this()
    {
        Data = data;
    }
}