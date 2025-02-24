using System;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.crm;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Persons.Catalogs;

internal sealed class GetPersonCatalogsQueryHandler : IQueryHandler<GetPersonCatalogsQuery, Dictionary<string, List<CatalogResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    public GetPersonCatalogsQueryHandler(IApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<Dictionary<string, List<CatalogResponse>>>> Handle(GetPersonCatalogsQuery request, CancellationToken cancellationToken)
    {
        Dictionary<string, List<CatalogResponse>> catalogs = [];
        foreach (var catalogName in request.CatalogNames)
        {
            var catalog = await GetCatalogByName(catalogName, cancellationToken);
            catalogs.Add(catalogName, catalog);
        }
        return Result.Success(catalogs);
    }

    private async Task<List<CatalogResponse>> GetCatalogByName(string catalogName, CancellationToken cancellationToken)
    {
        return catalogName switch
        {
            "address_type" => await _dbContext.Query<AddressType>()
                .AsNoTracking()
                .Where(x => x.Active && x.Visible)
                .Select(x => new CatalogResponse(x.Id, null, x.Name))
                .ToListAsync(cancellationToken),
            "person_type" => await _dbContext.Query<PersonType>()
                .AsNoTracking()
                .Where(x => x.Active && x.Visible)
                .Select(x => new CatalogResponse(x.Id, null, x.Name))
                .ToListAsync(cancellationToken),
            "attributes_data_type" => await _dbContext.Query<AttributesDataType>()
                .AsNoTracking()
                .Where(x => x.Active && x.Visible)
                .Select(x => new CatalogResponse(x.Id, null, x.Name))
                .ToListAsync(cancellationToken),
            "person_status" => await _dbContext.Query<PersonStatus>()
                .AsNoTracking()
                .Where(x => x.Active && x.Visible)
                .Select(x => new CatalogResponse(0, x.Id, x.Name ?? "N/D"))
                .ToListAsync(cancellationToken),
            "relationship_type" => await _dbContext.Query<RelationshipType>()
                .AsNoTracking()
                .Where(x => x.Active && x.Visible)
                .Select(x => new CatalogResponse(x.Id, null, x.Name ?? "N/D"))
                .ToListAsync(cancellationToken),
            _ => throw new NotImplementedException()
        };
    }
}
