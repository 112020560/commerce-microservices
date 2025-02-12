using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class Module
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreateAt { get; set; }

    public bool IsEnable { get; set; }

    public virtual ICollection<MenuOption> MenuOptions { get; set; } = new List<MenuOption>();

    public virtual ICollection<RoleModule> RoleModules { get; set; } = new List<RoleModule>();
}
