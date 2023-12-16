using NetCore.WebApiCommon.Core.Common.Interfaces;
using NetCore.WebApiCommon.Core.DAL.Interfaces;

namespace NetCore.WebApiCommon.Core.DAL.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly IAppDbContext _dbContext;
    private readonly ILogService _logService;

    public UnitOfWork(IAppDbContext dbContext, ILogService logService)
    {
        _dbContext = dbContext;
        _logService = logService;
    }
    
    public void Commit()
    {
        _dbContext.SaveChanges();
    }

    public Task CommitAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

    public void Rollback()
    {
        Dispose();
    }

    public Task RollbackAsync()
    {
        Dispose();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        try
        {
            _logService.Info("Transaction rollback - Do nothing");
            GC.SuppressFinalize(this);
        }
        catch (Exception e)
        {
            _logService.Error(e);
        }
    }
}