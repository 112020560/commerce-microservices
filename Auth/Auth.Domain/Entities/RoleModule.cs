namespace Domain.Models.Auth;

public partial class RoleModule
{
    public int RoleId { get; set; }

    public int ModuleId { get; set; }

    public bool CanRead { get; set; }

    public bool CanWrite { get; set; }

    public bool CanUpdate { get; set; }

    public bool CanDelete { get; set; }

    public virtual Module Module { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
