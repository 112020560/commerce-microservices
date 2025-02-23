namespace Application.Persons.Create;

public record CreatePersonRequest
{
    public Guid? Id { get; set; }
    public int TypeId { get; set; }

    public Guid StatusId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime BirthDate { get; set; }
    public List<PersonAttributte>? PersonAttributtes { get; set; }
    public List<PersonAddress>? PersonAddresses { get; set; }
}

public record PersonAttributte 
{
    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public int DataType { get; set; }
}


public record PersonAddress
{
    public string Street { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string? Complement { get; set; }

    public string Neighborhood { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string ZipCode { get; set; } = null!;
    public int Type { get; set; }

}