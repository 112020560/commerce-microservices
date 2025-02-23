using System;
using Application.Abstractions.Data.Crm;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories.Crm;

public class AttributeRepository: IAttributeRepository
{
    private readonly CommerceDbContext _commerceDbContext;
    public AttributeRepository(CommerceDbContext commerceDbContext)
    {
        _commerceDbContext=commerceDbContext;
    }
}
