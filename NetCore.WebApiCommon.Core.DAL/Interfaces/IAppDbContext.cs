﻿using Microsoft.EntityFrameworkCore;
using NetCore.WebApiCommon.Core.Entities;

namespace NetCore.WebApiCommon.Core.DAL.Interfaces;

public interface IAppDbContext
{
    DbSet<T> CreateSet<T, TKey>() where T : BaseEntity<TKey>;
    void Attach<T, TKey>(T entity) where T : BaseEntity<TKey>;
    void SetModified<T, TKey>(T entity) where T : BaseEntity<TKey>;
    void SetDeleted<T, TKey>(T entity) where T : BaseEntity<TKey>;
    void Refresh<T, TKey>(T entity) where T : BaseEntity<TKey>;
    void Update<T, TKey>(T entity) where T : BaseEntity<TKey>;
    void SaveChanges();
    Task SaveChangesAsync();
}