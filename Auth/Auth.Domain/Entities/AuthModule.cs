namespace Auth.Domain.Entities;

public partial class AuthModule
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateAt { get; set; }

    public bool IsEnable { get; set; }

    public virtual ICollection<AuthMenuOption> AuthMenuOptions { get; set; } = new List<AuthMenuOption>();

    public virtual ICollection<AuthRoleModule> AuthRoleModules { get; set; } = new List<AuthRoleModule>();
}
