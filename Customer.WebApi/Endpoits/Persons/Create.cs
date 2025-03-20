using Customer.WebApi.Extensions;
using MediatR;
using Persons.Application.Create;
using SharedKernel;

namespace Customer.WebApi.Endpoits.Persons;

public sealed class Create: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/person", async (CreatePersonRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                Result<ResponseObject> result = await sender.Send(new CreatePersonCommand(request), cancellationToken);

                return result.Match(
                    success => Results.Ok(success),
                    error => Results.BadRequest(error)
                );
            })
            .WithName("Create")
            .WithOpenApi();
    }
}