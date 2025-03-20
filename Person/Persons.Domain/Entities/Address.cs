using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.crm;

namespace Persons.Domain.Entities;

public partial class Address
{
    public Guid Id { get; set; }

    public Guid? PersonId { get; set; }

    public string Address1 { get; set; } = null!;

    public string? City { get; set; }

    public string? Country { get; set; }

    [Column(name:"state")]
    public string?  State { get; set; }

    public string? PostalCode { get; set; }

    public int Type { get; set; }

    public virtual global::Domain.Models.crm.Person? Person { get; set; }

    public virtual AddressType TypeNavigation { get; set; } = null!;
}
