using System;
using Application.Abstractions.Data.Crm;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories.Crm;

internal sealed class PersonRelationshipRepository: IPersonRelationshipRepository
{
    private readonly CommerceDbContext _crmConnection;
    public PersonRelationshipRepository(CommerceDbContext commerceDbContext)
    {
        _crmConnection = commerceDbContext ?? throw new ArgumentNullException(nameof(commerceDbContext));
    }
}
