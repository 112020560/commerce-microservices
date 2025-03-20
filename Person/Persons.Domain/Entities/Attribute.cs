using Domain.Models.crm;

namespace Person.Domain.Entities;

public partial class Attribute
{
    public Guid Id { get; set; }

    public Guid? PersonId { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public int DataType { get; set; }

    public virtual AttributesDataType DataTypeNavigation { get; set; } = null!;

    public virtual global::Domain.Models.crm.Person? Person { get; set; }
}
