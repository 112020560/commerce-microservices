using Domain.Models.Auth;

namespace Application.Abstractions.Data.Auth;

public interface IAuthUnitOfWork
{
    public IRepository<User> UserRepository { get; }
    public IRepository<Role> RoleRepository { get; }
    public IRepository<UserRole> UserRoleRepository { get; }
}
