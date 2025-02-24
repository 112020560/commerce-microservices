using Application.Persons.Get;
using Auth.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Customer.WebApi.Endpoits.Persons;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/person", async ([FromQuery] int pageSize, [FromQuery] int pageNumber, ISender sender, CancellationToken cancellationToken) =>
            {
                var request = new GetCustomerRequest(null, 0, null, null, pageSize, pageNumber);
                Result<ResponseObject<List<CustomPersonResponse>>> result = await sender.Send(new GetAllPersonsQuery(request), cancellationToken);

                return result.Match(
                    success => Results.Ok(success),
                    error => Results.BadRequest(error)
                );
            })
            .WithName("GetAll")
            .WithOpenApi();

        app.MapGet("/person/filter", async ([AsParameters] GetCustomerRequest query, ISender sender, CancellationToken cancellationToken) =>
            {
                Result<ResponseObject<List<CustomPersonResponse>>> result = await sender.Send(new GetPersonByFilterQuery(query), cancellationToken);

                return result.Match(
                    success => Results.Ok(success),
                    error => Results.BadRequest(error)
                );
            })
            .WithName("GetByFilter")
            .WithOpenApi();
    }
}
