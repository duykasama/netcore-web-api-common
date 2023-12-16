using NetCore.WebApiCommon.Core.Common.Interfaces;

namespace NetCore.WebApiCommon.Core.Common.Helpers;

public static class LogHelper
{
    private static ILogService _loggerservice;

    public static void InitLoggerService(ILogService loggerService)
    {
        _loggerservice = loggerService;
    }

    public static void WriteInfo(string message)
    {
        _loggerservice.Info(message);
    }
    
    public static void WriteError(string message)
    {
        _loggerservice.Error(message);
    }
}