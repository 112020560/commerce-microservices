using System;
using System.Collections.Generic;

namespace Auth.Infrastructure;

public partial class AuthMenuOption
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Url { get; set; }

    public string? Icon { get; set; }

    public Guid ModuleId { get; set; }

    public int? Order { get; set; }

    public bool? Visible { get; set; }

    public virtual AuthModule Module { get; set; } = null!;

    public virtual ICollection<AuthRole> Roles { get; set; } = new List<AuthRole>();
}
