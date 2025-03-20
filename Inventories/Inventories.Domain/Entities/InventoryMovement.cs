namespace Inventories.Domain.Entities;

public partial class InventoryMovement
{
    public Guid Id { get; set; }

    public Guid? DocumentTypeId { get; set; }

    public Guid? DocumentId { get; set; }

    public Guid? SourceLocationId { get; set; }

    public Guid? DestinationLocationId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Description { get; set; }

    public virtual Location? DestinationLocation { get; set; }

    public virtual DocumentType? DocumentType { get; set; }

    public virtual ICollection<InventoryMovementDetail> InventoryMovementDetails { get; set; } = new List<InventoryMovementDetail>();

    public virtual Location? SourceLocation { get; set; }
}
