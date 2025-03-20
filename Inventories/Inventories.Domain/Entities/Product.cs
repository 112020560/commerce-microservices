namespace Domain.Models.retail;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Sku { get; set; } = null!;

    public string? Barcode { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? BrandId { get; set; }

    public decimal Price { get; set; }

    public decimal Cost { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<DocumentDetail> DocumentDetails { get; set; } = new List<DocumentDetail>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<InventoryMovementDetail> InventoryMovementDetails { get; set; } = new List<InventoryMovementDetail>();
}
