using Auth.Application.Abstractions;
using Auth.Application.Abstractions.Messaging;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel;
using SharedKernel.Exceptions;

namespace Auth.Application.Users.Login;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, ResponseObject<LoginResponse>>
{
    private readonly ILogger<LoginUserCommandHandler> _logger;
    private readonly IApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITokenProvider _tokenProvider;

    public LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger, IApplicationDbContext dbContext,IPasswordHasher passwordHasher, IDateTimeProvider dateTimeProvider, ITokenProvider tokenProvider)
    {
        _logger = logger;
        _passwordHasher = passwordHasher;
        _dateTimeProvider = dateTimeProvider;
        _tokenProvider = tokenProvider;
        _dbContext = dbContext;
    }
    public async Task<Result<ResponseObject<LoginResponse>>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Query<User>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Username == request.UserNamne, cancellationToken: cancellationToken)
                ?? throw new UnAuthorizerException("User not found");
        
        if (!_passwordHasher.Verify(request.Password, user.Password))
        {
            throw new UnAuthorizerException("Invalid Password");
        }

        _logger.LogInformation("User {Username} logged in successfully.", user.Username);

        return new ResponseObject<LoginResponse>
        {
            IsSuccess = true,
            Data = new LoginResponse { AccessToken = _tokenProvider.Create(user) },
            Message = "Login Successful",
            Timestamp = _dateTimeProvider.UtcNow

        };
    }
}
