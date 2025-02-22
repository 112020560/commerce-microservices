namespace Domain.Models.retail;

public partial class DocumentStatus
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public bool Visible { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
