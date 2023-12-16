using Autofac;
using NetCore.WebApiCommon.Core.Common.Interfaces;
using NetCore.WebApiCommon.Infrastructure.Common.Models;
using NLog;

namespace NetCore.WebApiCommon.Infrastructure.Common.Implementations;

public class NLogService : GenericService, ILogService
{
    private readonly ILogger _logger;
    
    public NLogService(ILifetimeScope scope) : base(scope)
    {
        _logger = LogManager.GetLogger(GlobalData.ModuleName);
    }
    
    public void Debug(string message)
    {
        _logger.Debug(message);
    }

    public void Debug(string message, params object[] args)
    {
        _logger.Debug(message, args);
    }

    public void Info(string message)
    {
        _logger.Info(message);
    }

    public void Info(string message, params object[] args)
    {
        _logger.Info(message, args);
    }

    public void Warn(string message)
    {
        _logger.Warn(message);
    }

    public void Warn(string message, params object[] args)
    {
        _logger.Warn(message, args);
    }

    public void Error(string message)
    {
        _logger.Error(message);
    }

    public void Error(string message, params object[] args)
    {
        _logger.Error(message, args);
    }

    public void Error(Exception ex)
    {
        _logger.Error(ex);
    }

    public void Fatal(string message)
    {
        _logger.Fatal(message);
    }

    public void Fatal(string message, params object[] args)
    {
        _logger.Fatal(message, args);
    }
}