namespace Domain.Models.crm;

public partial class RelationshipType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    public bool Visible { get; set; }

    public virtual ICollection<PersonRelationship> PersonRelationships { get; set; } = new List<PersonRelationship>();
}
