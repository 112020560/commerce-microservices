namespace Inventories.Domain.Entities;

public partial class InventoryMovementDetail
{
    public Guid Id { get; set; }

    public Guid? MovementId { get; set; }

    public Guid? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal? UnitCost { get; set; }

    public string? MovementType { get; set; }

    public virtual InventoryMovement? Movement { get; set; }

    public virtual Product? Product { get; set; }
}
