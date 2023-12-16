using NetCore.WebApiCommon.Core.Common.Interfaces;

namespace NetCore.WebApiCommon.Core.Common.Helpers;

public static class LogHelper
{
    private static ILogService? _logService;

    private static ILogService LogService
    {
        get
        {
            return _logService ??= DependencyInjectionHelper.ResolveService<ILogService>();
        }
    }

    public static void WriteInfo(string message)
    {
        LogService.Info(message);
    }
    
    public static void WriteError(string message)
    {
        LogService.Error(message);
    }
}