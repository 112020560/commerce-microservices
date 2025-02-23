using System;
using Application.Abstractions.Data.Retail;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories.Retail;

internal sealed class InventoryMovementRepository: IInventoryMovementRepository
{
    private readonly CommerceDbContext _dbContext;
    public InventoryMovementRepository(CommerceDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
}
