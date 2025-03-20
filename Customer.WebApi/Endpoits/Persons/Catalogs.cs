using Customer.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persons.Application.Catalogs;
using SharedKernel;

namespace Customer.WebApi.Endpoits.Persons;

public class Catalogs : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/catalogs", async ([FromQuery] string CatalogsName, ISender sender, CancellationToken cancellationToken) =>
            {
                var catalogsName =  CatalogsName.Split(',');
                Result<Dictionary<string, List<CatalogResponse>>> result = await sender.Send(new GetPersonCatalogsQuery(catalogsName), cancellationToken);

                return result.Match(
                    success => Results.Ok(success),
                    error => Results.BadRequest(error)
                );
            })
            .WithName("Catalogs")
            .WithOpenApi();
    }
}
