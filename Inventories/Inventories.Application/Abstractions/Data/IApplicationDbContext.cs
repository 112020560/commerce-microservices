using Inventories.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventories.Application.Abstractions.Data;

public interface IApplicationDbContext
{

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
    DbSet<Product> Products { get; set;}

    IQueryable<T> Query<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
