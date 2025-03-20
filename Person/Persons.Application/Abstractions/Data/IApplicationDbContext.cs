using Person.Domain.Entities;

namespace Person.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Address> Addresses { get;  set;}

    DbSet<AddressType> AddressTypes {get; set;}

    DbSet<Domain.Entities.Attribute> Attributes {get; set;}

    DbSet<AttributesDataType> AttributesDataTypes {get; set;}

    //DbSet<Document> Documents {get; set;}

    //DbSet<DocumentDetail> DocumentDetails {get; set;}
    

    DbSet<Person> Person { get; set; }

    DbSet<PersonRelationship> PersonRelationships {get; set;}

    DbSet<PersonType> PersonTypes {get; set;}

    IQueryable<T> Query<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
