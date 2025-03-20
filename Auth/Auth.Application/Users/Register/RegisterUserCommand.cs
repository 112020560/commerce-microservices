using Auth.Application.Abstractions.Messaging;
using SharedKernel;

namespace Auth.Application.Users.Register;

public sealed record RegisterUserCommand(
    string UserName, 
    string Password, 
    string FullName,
    string? SystemUser,
    Guid[]? Roles
    ): ICommand<ResponseObject>;
