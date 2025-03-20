namespace Domain.Models.retail;

public partial class DocumentDetail
{
    public Guid Id { get; set; }

    public Guid? DocumentId { get; set; }

    public Guid? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Total { get; set; }

    public virtual Document? Document { get; set; }

    public virtual Product? Product { get; set; }
}
