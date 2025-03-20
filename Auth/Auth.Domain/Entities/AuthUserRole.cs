namespace Auth.Domain.Entities;

public partial class AuthUserRole
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public DateTime? AssigmentDate { get; set; }

    public virtual AuthRole Role { get; set; } = null!;

    public virtual AuthUser User { get; set; } = null!;
}
