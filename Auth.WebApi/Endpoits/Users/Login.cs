using Auth.Application.Users.Login;
using Auth.WebApi.Extensions;
using MediatR;
using SharedKernel;

namespace Auth.WebApi.Endpoits.Users;

public class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (LoginUserCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            Result<ResponseObject<LoginResponse>> result = await sender.Send(request, cancellationToken);

            return result.Match(
                success => Results.Ok(success),
                error => Results.BadRequest(error)
            );
        })
        .WithName("Login")
        .WithOpenApi();
    }
}
