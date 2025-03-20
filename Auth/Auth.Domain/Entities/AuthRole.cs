namespace Auth.Domain.Entities;

public partial class AuthRole
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateAt { get; set; }

    public bool? IsEnable { get; set; }

    public virtual ICollection<AuthRoleModule> AuthRoleModules { get; set; } = new List<AuthRoleModule>();

    public virtual ICollection<AuthUserRole> AuthUserRoles { get; set; } = new List<AuthUserRole>();

    public virtual ICollection<AuthMenuOption> MenuOptions { get; set; } = new List<AuthMenuOption>();
}
