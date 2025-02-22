using Application.Abstractions.Data;
using Application.Abstractions.Data.Crm;
using Domain.Models.crm;
using Infrastructure.Persistence.Context;
using Attribute = Domain.Models.crm.Attribute;

namespace Infrastructure.Persistence.Repositories.Crm;

public class CrmUnitOfWork: ICrmUnitOfWork
{
    private readonly IRepository<Address>? _addresRepository;
    private readonly IRepository<AddressType>? _addressTypeRepository;
    private readonly IRepository<Attribute>? _attributeRepository;
    private readonly IRepository<AttributesDataType>? _attributeDataTypeRepository;
    private readonly IRepository<Person>? _personRepository;
    private readonly IRepository<PersonRelationship>? _personRelationshipRepository;
    private readonly IRepository<PersonType>? _personTypeRepository;
    private readonly IRepository<RelationshipType>? _relationshipTypeRepository;
    
    private readonly CommerceDbContext _commerceDbContext;
    public CrmUnitOfWork(CommerceDbContext commerceDbContext)
    {
        _commerceDbContext = commerceDbContext;
    }
    
    public IRepository<Address> AddresRepository { get => _addresRepository ?? new Repository<Address>(_commerceDbContext);}
    public IRepository<AddressType> AddressTypeRepository { get => _addressTypeRepository ?? new Repository<AddressType>(_commerceDbContext);}
    public IRepository<Attribute> AttributeRepository { get => _attributeRepository ?? new Repository<Attribute>(_commerceDbContext);}
    public IRepository<AttributesDataType> AttributeDataTypeRepository { get => _attributeDataTypeRepository ?? new Repository<AttributesDataType>(_commerceDbContext);}
    public IRepository<Person> PersonRepository { get => _personRepository ?? new Repository<Person>(_commerceDbContext);}
    public IRepository<PersonRelationship> PersonRelationshipRepository { get => _personRelationshipRepository ?? new Repository<PersonRelationship>(_commerceDbContext);}
    public IRepository<PersonType> PersonTypeRepository { get => _personTypeRepository ?? new Repository<PersonType>(_commerceDbContext);}
    public IRepository<RelationshipType> PelationshipTypeRepository { get => _relationshipTypeRepository ?? new Repository<RelationshipType>(_commerceDbContext);}
}