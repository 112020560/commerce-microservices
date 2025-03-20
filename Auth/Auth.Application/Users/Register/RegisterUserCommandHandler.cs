using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.Register;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, ResponseObject>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeProvider _dateTimeProvider;
    public RegisterUserCommandHandler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<ResponseObject>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        bool userExists = await _dbContext.Query<User>()
                                    .AsNoTracking()
                                    .AnyAsync(x => x.Username == request.UserName, cancellationToken);
        if (userExists)
        {
            return Result.Failure<ResponseObject>(new Error("Duplicated", "User already exists", ErrorType.Validation));
        }

        List<UserRole>? userRoles = request.Roles?.Select(x => new UserRole { RoleId = x }).ToList();
        string createdBy = request.SystemUser ?? "system";

        var user = new User()
        {
            Username = request.UserName,
            Password = _passwordHasher.Hash(request.Password),
            FullName = request.FullName,
            CreateAt = _dateTimeProvider.UtcNow,
            CreateBy = createdBy,
            UpdateAt = _dateTimeProvider.UtcNow,
            UpdateBy = createdBy,
            IsEnable = true,
            UserRoles = userRoles ?? []
        };

        var entity = await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseObject {
            IsSuccess = true,
            Message = "User created successfully",
            Timestamp = _dateTimeProvider.UtcNow,
        };
    }
}