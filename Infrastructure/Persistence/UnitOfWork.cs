using System;
using Application.Abstractions.Data;
using Application.Abstractions.Data.Auth;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly CommerceDbContext _dbContext;
    public UnitOfWork(CommerceDbContext commerceDbContext)
    {
        _dbContext = commerceDbContext ?? throw new ArgumentNullException(nameof(commerceDbContext));
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
