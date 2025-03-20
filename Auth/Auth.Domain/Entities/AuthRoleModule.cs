namespace Auth.Domain.Entities;

public partial class AuthRoleModule
{
    public Guid RoleId { get; set; }

    public Guid ModuleId { get; set; }

    public bool CanRead { get; set; }

    public bool CanWrite { get; set; }

    public bool CanUpdate { get; set; }

    public bool CanDelete { get; set; }

    public virtual AuthModule Module { get; set; } = null!;

    public virtual AuthRole Role { get; set; } = null!;
}
