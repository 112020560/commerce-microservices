using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Abstractions.Data;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task CreateManyAsync(IList<TEntity> entities);
    Task UpdateAsync(Expression<Func<TEntity, bool>> filter,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setProperties);
    Task DeleteAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity?> FindOneByFilterAsync(Expression<Func<TEntity, bool>> filter);
    Task<IList<TEntity>> FindByFilterAsync(Expression<Func<TEntity, bool>> filter);
    Task<IList<TEntity>> FindAllAsync();
    Task UpdateCommonAsync(TEntity entity);
    Task<bool>  ExistsAsync(Expression<Func<TEntity, bool>> filter,  CancellationToken cancellationToken);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);
}
