using Auth.Application.Abstractions.Messaging;
using SharedKernel;

namespace Auth.Application.Users.Login;

public record LoginUserCommand(string UserNamne, string Password): ICommand<ResponseObject<LoginResponse>>;
