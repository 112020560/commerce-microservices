using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.crm;

[Table(name:"person_status", Schema ="crm")]
public class PersonStatus
{
    [Column(name:"id")]
    public Guid Id { get; set; }
    [Column(name:"name")]
    public string? Name { get; set; }
    [Column(name:"description")]
    public string? Description { get; set; }
    [Column(name:"visible")]
    public bool Visible { get; set; }
    [Column(name:"active")]
    public bool Active { get; set; }
}
