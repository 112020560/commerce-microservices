namespace Domain.Models.retail;

public partial class Location
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<InventoryMovement> InventoryMovementDestinationLocations { get; set; } = new List<InventoryMovement>();

    public virtual ICollection<InventoryMovement> InventoryMovementSourceLocations { get; set; } = new List<InventoryMovement>();
}
