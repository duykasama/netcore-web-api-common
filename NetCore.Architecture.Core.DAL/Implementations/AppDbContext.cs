using Microsoft.EntityFrameworkCore;
using NetCore.Architecture.Core.DAL.Interfaces;
using NetCore.Architecture.Core.Entities;

namespace NetCore.Architecture.Core.DAL.Implementations;

public abstract class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<T> CreateSet<T, TKey>() where T : BaseEntity<TKey>
    {
        return base.Set<T>();
    }

    public void Attach<T, TKey>(T entity) where T : BaseEntity<TKey>
    {
        base.Entry(entity).State = EntityState.Unchanged;
    }

    public void SetModified<T, TKey>(T entity) where T : BaseEntity<TKey>
    {
        base.Entry(entity).State = EntityState.Modified;
    }

    public void SetDeleted<T, TKey>(T entity) where T : BaseEntity<TKey>
    {
        base.Entry(entity).State = EntityState.Deleted;
    }

    public void Refresh<T, TKey>(T entity) where T : BaseEntity<TKey>
    {
        base.Entry(entity).Reload();;
    }

    public void Update<T, TKey>(T entity) where T : BaseEntity<TKey>
    {
        base.Update(entity);
    }

    public new void SaveChanges()
    {
        base.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}