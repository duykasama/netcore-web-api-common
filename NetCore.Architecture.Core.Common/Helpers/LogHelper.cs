using NetCore.Architecture.Core.Common.Interfaces;

namespace NetCore.Architecture.Core.Common.Helpers;

public static class LogHelper
{
    private static ILogService _loggerservice;

    public static void InitLoggerService(ILogService loggerService)
    {
        _loggerservice = loggerService;
    }
    // cannot inject ILoggerService to LogHelper

    public static void WriteInfo(string message)
    {
        _loggerservice.Info(message);
    }
}