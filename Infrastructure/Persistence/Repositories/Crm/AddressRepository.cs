using System;
using Application.Abstractions.Data.Crm;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories.Crm.Repositories;

internal sealed class AddressRepository: IAddressRepository
{
    private readonly CommerceDbContext _commerDbContext;
    public AddressRepository(CommerceDbContext commerDbContext)
    {
        _commerDbContext = commerDbContext ?? throw new ArgumentNullException(nameof(commerDbContext));
    }    
}
