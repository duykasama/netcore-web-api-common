﻿using System.Linq.Expressions;
using NetCore.Architecture.Core.Entities;

namespace NetCore.Architecture.Core.DAL.Interfaces;

public interface IRepository<T, TKey> where T : BaseEntity<TKey>
{
    void Add(T entity);
    Task AddAsync(T entity);
    void AddMany(IEnumerable<T> entities);
    Task AddManyAsync(IEnumerable<T> entities);
    
    void Update(T entity);
    Task UpdateAsync(T entity);
    void UpdateMany(IEnumerable<T> entities);
    void UpdateMany(Expression<Func<T, bool>> predicate);
    Task UpdateManyAsync(IEnumerable<T> entities);
    Task UpdateManyAsync(Expression<Func<T, bool>> predicate);
    
    void Delete(object id);
    Task DeleteAsync(object id);
    void DeleteMany(IEnumerable<object> ids);
    void DeleteMany(Expression<Func<T, bool>> predicate);
    Task DeleteManyAsync(IEnumerable<object> ids);
    Task DeleteManyAsync(Expression<Func<T, bool>> predicate);
    
    IQueryable<T> GetAll();
    Task<IQueryable<T>> GetAllAsync();
    T? Get(object id);
    T? Get(Expression<Func<T, bool>> predicate);
    Task<T?> GetAsync(object id);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    
    long Count(Expression<Func<T, bool>> predicate);
    Task<long> CountAsync(Expression<Func<T, bool>> predicate);
    
    bool Exists(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}