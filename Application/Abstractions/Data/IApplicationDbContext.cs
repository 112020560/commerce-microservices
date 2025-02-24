using System;
using Domain.Models.Auth;
using Domain.Models.crm;
using Domain.Models.retail;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Address> Addresses { get;  set;}

    DbSet<AddressType> AddressTypes {get; set;}

    DbSet<Domain.Models.crm.Attribute> Attributes {get; set;}

    DbSet<AttributesDataType> AttributesDataTypes {get; set;}

    DbSet<Brand> Brands {get; set;}

    DbSet<Category> Categories {get; set;}

    DbSet<Document> Documents {get; set;}

    DbSet<DocumentDetail> DocumentDetails {get; set;}

    DbSet<DocumentStatus> DocumentStatuses {get; set;}

    DbSet<DocumentType> DocumentTypes {get; set;}

    DbSet<Inventory> Inventories {get; set;}

    DbSet<InventoryMovement> InventoryMovements {get; set;}

    DbSet<InventoryMovementDetail> InventoryMovementDetails {get; set;}

    DbSet<Location> Locations {get; set;}

    DbSet<MenuOption> MenuOptions {get; set;}

    DbSet<Module> Modules {get; set;}

    DbSet<Person> Person { get; set; }

    DbSet<PersonRelationship> PersonRelationships {get; set;}

    DbSet<PersonType> PersonTypes {get; set;}

    DbSet<Product> Products {get; set;}

    DbSet<RelationshipType> RelationshipTypes {get; set;}

    DbSet<Role> Roles {get; set;}

    DbSet<RoleModule> RoleModules {get; set;}

    DbSet<User> Users {get; set;}

    DbSet<UserRole> UserRoles {get; set;}
    DbSet<PersonStatus> PersonStatus {get; set;}

    IQueryable<T> Query<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
