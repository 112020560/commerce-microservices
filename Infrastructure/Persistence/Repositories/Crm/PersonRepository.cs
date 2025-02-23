using System;
using Application.Abstractions.Data.Crm;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories.Crm;

internal sealed class PersonRepository: IPersonRepository
{
    private readonly CommerceDbContext _commerceDbContext;
    public PersonRepository(CommerceDbContext commerceDbContext)
    {
        _commerceDbContext = commerceDbContext;
    }
}
