using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class UserRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime? AssignmentDate { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
