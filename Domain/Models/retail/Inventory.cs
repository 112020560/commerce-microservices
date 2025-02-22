namespace Domain.Models.retail;

public partial class Inventory
{
    public Guid Id { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? LocationId { get; set; }

    public int Quantity { get; set; }

    public int? ReservedQuantity { get; set; }

    public int? MinStock { get; set; }

    public int? MaxStock { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Location? Location { get; set; }

    public virtual Product? Product { get; set; }
}
