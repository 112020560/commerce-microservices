using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class User
{
    public int Id { get; set; }

    /// <summary>
    /// username of a system
    /// </summary>
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public bool IsEnable { get; set; }

    public DateTime CreateAt { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime? UpdateAt { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
