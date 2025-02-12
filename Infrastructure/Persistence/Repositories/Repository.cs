using System;
using System.Linq.Expressions;
using Application.Abstractions.Data;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly CommerceDbContext _dbcontext;
    public Repository(CommerceDbContext dbContext)
    {
        _dbcontext = dbContext;
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
    {
        return _dbcontext.Set<TEntity>().Where(filter).CountAsync();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var inserted = await _dbcontext.Set<TEntity>().AddAsync(entity);
        await _dbcontext.SaveChangesAsync();
        return inserted.Entity;
    }

    public async Task CreateManyAsync(IList<TEntity> entities)
    {
        await _dbcontext.Set<TEntity>().AddRangeAsync(entities);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter)
    {
        await _dbcontext.Set<TEntity>().Where(filter).ExecuteDeleteAsync();
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        return _dbcontext.Set<TEntity>().AnyAsync(filter);
    }

    public async Task<IList<TEntity>> FindAllAsync()
    {
        return await _dbcontext.Set<TEntity>().ToListAsync();
    }

    public async Task<IList<TEntity>> FindByFilterAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbcontext.Set<TEntity>().Where(filter).ToListAsync();
    }

    public async Task<TEntity?> FindOneByFilterAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbcontext.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setProperties)
    {
        await _dbcontext.Set<TEntity>().Where(filter).ExecuteUpdateAsync(setProperties);
    }

    public async Task UpdateCommonAsync(TEntity entity)
    {
        _dbcontext.Set<TEntity>().Update(entity);
        await _dbcontext.SaveChangesAsync();
    }
}
