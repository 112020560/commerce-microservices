using Domain.Models.crm;
using Attribute = Domain.Models.crm.Attribute;

namespace Application.Abstractions.Data.Crm;

public interface ICrmUnitOfWork
{
    public IRepository<Address> AddresRepository { get; }
    public IRepository<AddressType> AddressTypeRepository { get ;}
    public IRepository<Attribute> AttributeRepository { get ;}
    public IRepository<AttributesDataType> AttributeDataTypeRepository { get;}
    public IRepository<Person> PersonRepository { get ;}
    public IRepository<PersonRelationship> PersonRelationshipRepository { get;}
    public IRepository<PersonType> PersonTypeRepository { get ;}
    public IRepository<RelationshipType> PelationshipTypeRepository { get ;}
}