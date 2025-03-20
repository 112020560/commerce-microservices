namespace Persons.Domain.Entities;

public partial class AddressType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    public bool Visible { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
