using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Users.Login;

public record LoginUserCommand(string UserNamne, string Password): ICommand<ResponseObject<LoginResponse>>;
