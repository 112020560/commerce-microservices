namespace Application.Products;

public record CreateProductRequest
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
}
