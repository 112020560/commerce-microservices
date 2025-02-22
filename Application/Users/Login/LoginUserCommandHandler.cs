using System;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data.Auth;
using Application.Abstractions.Messaging;
using Microsoft.Extensions.Logging;
using SharedKernel;
using SharedKernel.Exceptions;

namespace Application.Users.Login;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, ResponseObject<LoginResponse>>
{
    private readonly ILogger<LoginUserCommandHandler> _logger;
    private readonly IAuthUnitOfWork _authUnitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITokenProvider _tokenProvider;

    public LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger, IAuthUnitOfWork authUnitOfWork, IPasswordHasher passwordHasher, IDateTimeProvider dateTimeProvider, ITokenProvider tokenProvider)
    {
        _logger = logger;
        _authUnitOfWork = authUnitOfWork;
        _passwordHasher = passwordHasher;
        _dateTimeProvider = dateTimeProvider;
        _tokenProvider = tokenProvider;
    }
    public async Task<Result<ResponseObject<LoginResponse>>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _authUnitOfWork.UserRepository.FindOneByFilterAsync(x => x.Username == request.UserNamne) ?? throw new UnAuthorizerException("User not found");
        bool verified = _passwordHasher.Verify(request.Password, user.Password);
        if (!verified)
        {
            throw new UnAuthorizerException("Invalid Password");
        }

        return new ResponseObject<LoginResponse>
        {
            IsSuccess = true,
            Data = new LoginResponse { AccessToken = _tokenProvider.Create(user) },
            Message = "Login Successful",
            Timestamp = _dateTimeProvider.UtcNow

        };
    }
}
