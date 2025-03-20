using System.Text.Json;
using Auth.Application.Abstractions;
using Auth.Application.Abstractions.Messaging;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Constants;
using SharedKernel.Contracts;

namespace Auth.Application.Users.Register;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, ResponseObject>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IProducerService _producerService;
    public RegisterUserCommandHandler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher, IDateTimeProvider dateTimeProvider, IProducerService producerService)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _dateTimeProvider = dateTimeProvider;
        _producerService = producerService;
    }
    public async Task<Result<ResponseObject>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        bool userExists = await _dbContext.Query<AuthUser>()
                                    .AsNoTracking()
                                    .AnyAsync(x => x.UserName == request.UserName, cancellationToken);
        if (userExists)
        {
            //return Result.Failure<ResponseObject>(new Error("Duplicated", "User already exists", ErrorType.Validation));
            return new Result<ResponseObject>(new ResponseObject
            {
                IsSuccess = false,
                Message = "User already exists",
                Timestamp = _dateTimeProvider.UtcNow
            }, true, new Error("Duplicated", "User already exists", ErrorType.Conflict));
        }

        List<AuthUserRole>? userRoles = request.Roles?.Select(x => new AuthUserRole { RoleId = x }).ToList();
        string createdBy = request.SystemUser ?? "system";

        var user = new AuthUser()
        {
            UserName = request.UserName,
            Password = _passwordHasher.Hash(request.Password),
            Name = request.FullName,
            CreateAt = _dateTimeProvider.UtcNow,
            CreateBy = createdBy,
            UpdateAt = _dateTimeProvider.UtcNow,
            UpdateBy = createdBy,
            IsEnable = true,
            AuthUserRoles = userRoles ?? []
        };

        var entity = await _dbContext.AuthUsers.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _producerService.SendCommand<UserRegisterEvent>(
            new UserRegisterEvent(Guid.NewGuid(),  "UserCreate", request), 
            Queues.USERS_EVENTSOURCING_QUEUE, Guid.NewGuid().ToString("N"), cancellationToken);
        
        return new Result<ResponseObject>(new ResponseObject
        {
            IsSuccess = true,
            Message = "User created successfully",
            Timestamp = _dateTimeProvider.UtcNow
        }, true, Error.None);
        
        // return new ResponseObject {
        //     IsSuccess = true,
        //     Message = "User created successfully",
        //     Timestamp = _dateTimeProvider.UtcNow,
        // };
    }
}