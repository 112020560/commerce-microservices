using System.Collections;

namespace Inventories.Domain.Entities;

public partial class Brand
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public BitArray Active { get; set; } = null!;

    public BitArray Visible { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
