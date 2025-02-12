namespace Domain.Models.Auth;

public partial class MenuOption
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Url { get; set; }

    public string? Icon { get; set; }

    public int ModuleId { get; set; }

    public int? Order { get; set; }

    public bool? Visible { get; set; }

    public virtual Module Module { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
