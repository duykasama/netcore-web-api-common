using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;
using NetCore.Architecture.Core.Common.Constants;
using NetCore.Architecture.Core.Common.Exceptions;

namespace NetCore.Architecture.Core.Common.Helpers;

public static class DataAccessHelper
{
    private static IConfiguration? _configuration;
    private static string? _connectionString;

    private static IConfiguration Configuration
    {
        get { return _configuration ??= DependencyInjectionHelper.ResolveService<IConfiguration>(); }
    }
    
    public static string GetConnectionString(string connectionName)
    {
        _connectionString = Configuration.GetConnectionString(connectionName) ?? throw new MissingConnectionStringException("Cannot find the specified connection string");
        return _connectionString;
    }

    public static string GetDefaultConnectionString()
    {
        return GetConnectionString(DataAccessConstants.DEFAULT_CONNECTION_NAME);
    }

    public static void MigrateDatabase(string assemblyName)
    {
        var connection = _connectionString ?? GetDefaultConnectionString();
        EnsureDatabase.For.SqlDatabase(connection);
        
        var upgradeEngine = DeployChanges.To.SqlDatabase(connection)
            .WithScriptsEmbeddedInAssembly(Assembly.Load(assemblyName))
            .LogToConsole()
            .Build();
        var scripts = upgradeEngine.GetDiscoveredScripts();
        if (scripts.Any())
        {
            upgradeEngine.PerformUpgrade();
        }
        else
        {
            LogHelper.WriteInfo("No scripts found");
        }
    }
}