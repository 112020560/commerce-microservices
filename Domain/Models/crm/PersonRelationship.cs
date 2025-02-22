namespace Domain.Models.crm;

public partial class PersonRelationship
{
    public Guid Id { get; set; }

    public Guid? PersonSource { get; set; }

    public Guid? PersonTarget { get; set; }

    public int RelationshipType { get; set; }

    public virtual Person? PersonSourceNavigation { get; set; }

    public virtual Person? PersonTargetNavigation { get; set; }

    public virtual RelationshipType RelationshipTypeNavigation { get; set; } = null!;
}
