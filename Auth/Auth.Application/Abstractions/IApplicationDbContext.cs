using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<AuthMenuOption> AuthMenuOptions { get; set; }

    DbSet<AuthModule> AuthModules { get; set; }

    DbSet<AuthRole> AuthRoles { get; set; }

    DbSet<AuthRoleModule> AuthRoleModules { get; set; }

    DbSet<AuthUser> AuthUsers { get; set; }

    DbSet<AuthUserRole> AuthUserRoles { get; set; }
    // DbSet<MenuOption> MenuOptions {get; set;}

    // DbSet<Module> Modules {get; set;}
    // DbSet<Role> Roles {get; set;}

    // DbSet<RoleModule> RoleModules {get; set;}

    // DbSet<User> Users {get; set;}

    // DbSet<UserRole> UserRoles {get; set;}

    IQueryable<T> Query<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
