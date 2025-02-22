using Application.Users.Register;
using Auth.WebApi.Extensions;
using MediatR;
using SharedKernel;

namespace Auth.WebApi.Endpoits.Users;

public sealed class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (RegisterUserCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            Result<ResponseObject> result = await sender.Send(request, cancellationToken);

            return result.Match(
                success => Results.Ok(success),
                error => Results.BadRequest(error)
            );
        })
        .WithName("Register")
        .WithOpenApi();
    }
}
