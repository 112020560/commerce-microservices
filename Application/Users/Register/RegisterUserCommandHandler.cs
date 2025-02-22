using Application.Abstractions.Authentication;
using Application.Abstractions.Data.Auth;
using Application.Abstractions.Messaging;
using Domain.Models.Auth;
using SharedKernel;

namespace Application.Users.Register;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, ResponseObject>
{
    private readonly IAuthUnitOfWork _authUnitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeProvider _dateTimeProvider;
    public RegisterUserCommandHandler(IAuthUnitOfWork authUnitOfWork, IPasswordHasher passwordHasher, IDateTimeProvider dateTimeProvider)
    {
        _authUnitOfWork = authUnitOfWork;
        _passwordHasher = passwordHasher;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<ResponseObject>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _authUnitOfWork.UserRepository.ExistsAsync(x => x.Username == request.UserName, cancellationToken))
        {
            return Result.Failure<ResponseObject>(new Error("Duplicated", "User already exists", ErrorType.Validation));
        }

        List<UserRole> userRoles = [];
        if (request.Roles is not null && request.Roles.Length > 0)
        {
            userRoles = request.Roles.Select(x => new UserRole() { RoleId = x }).ToList();
        }

        var user = new User()
        {
            Username = request.UserName,
            Password = _passwordHasher.Hash(request.Password),
            FullName = request.FullName,
            CreateAt = _dateTimeProvider.UtcNow,
            CreateBy = request.SystemUser ?? "system",
            UpdateAt = _dateTimeProvider.UtcNow,
            UpdateBy = request.SystemUser ?? "system",
            IsEnable = true,
            UserRoles = userRoles
        };

        var entity = await _authUnitOfWork.UserRepository.CreateAsync(user);

        return new ResponseObject {
            IsSuccess = true,
            Message = "User created successfully",
            Timestamp = _dateTimeProvider.UtcNow,
        };
    }
}