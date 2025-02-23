using Domain.Models.retail;
using Attribute = Domain.Models.crm.Attribute;

namespace Domain.Models.crm;

public partial class Person
{
    public Guid Id { get; set; }

    public int TypeId { get; set; }

    public Guid StatusId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<PersonRelationship> PersonRelationshipPersonSourceNavigations { get; set; } = new List<PersonRelationship>();

    public virtual ICollection<PersonRelationship> PersonRelationshipPersonTargetNavigations { get; set; } = new List<PersonRelationship>();
}
