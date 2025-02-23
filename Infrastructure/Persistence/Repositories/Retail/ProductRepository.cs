using System;
using Application.Abstractions.Data.Retail;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories.Retail;

internal sealed class ProductRepository: IProductRepository
{
    private readonly CommerceDbContext _dbContext;
    public ProductRepository(CommerceDbContext commerceDbContext)
    {
        _dbContext = commerceDbContext ?? throw new ArgumentNullException(nameof(commerceDbContext));
    }
}
