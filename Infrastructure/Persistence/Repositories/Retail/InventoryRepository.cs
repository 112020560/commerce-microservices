using System;
using Application.Abstractions.Data.Retail;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories.Retail;

internal sealed class InventoryRepository: IInventoryRepository
{
    private readonly CommerceDbContext _dbContext;
    public InventoryRepository(CommerceDbContext commerceDbContext)
    {
        _dbContext = commerceDbContext ?? throw new ArgumentNullException(nameof(commerceDbContext));
    }
}
