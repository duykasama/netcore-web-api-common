namespace NetCore.WebApiCommon.Core.DAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void Commit();
    Task CommitAsync();
    void Rollback();
    Task RollbackAsync();
}