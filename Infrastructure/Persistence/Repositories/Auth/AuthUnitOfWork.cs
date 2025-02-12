using Application.Abstractions.Data;
using Application.Abstractions.Data.Auth;
using Domain.Models.Auth;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories.Auth;

public class AuthUnitOfWork: IAuthUnitOfWork
{
    private readonly IRepository<User>? _userRepository;
    private readonly IRepository<Role>? _roleRepository;
    private readonly IRepository<UserRole>? _userRoleRepository;
    private readonly CommerceDbContext _commerceDbContext;
    public AuthUnitOfWork(CommerceDbContext dbContext)
    {
        _commerceDbContext = dbContext;
    }

    public IRepository<User> UserRepository { get => _userRepository ?? new Repository<User>(_commerceDbContext);}
    public IRepository<Role> RoleRepository { get => _roleRepository ?? new Repository<Role>(_commerceDbContext); }
    public IRepository<UserRole> UserRoleRepository { get => _userRoleRepository ?? new Repository<UserRole>(_commerceDbContext); }
}
