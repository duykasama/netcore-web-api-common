namespace NetCore.Architecture.Api.Models;

public class AppApiActionResult
{
    public bool IsSuccess { get; set; }
    public object Data { get; set; }
    public string Detail { get; set; }

    public AppApiActionResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
    
    public AppApiActionResult(bool isSuccess, string detail)
    {
        IsSuccess = isSuccess;
        Detail = detail;
    }

    public AppApiActionResult() : this(true)
    {
    }
}