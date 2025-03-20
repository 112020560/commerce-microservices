using Inventories.Application.Abstractions.Data;
using Inventories.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventories.Infrastructure.Persistence;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentDetail> DocumentDetails { get; set; }
    public DbSet<DocumentStatus> DocumentStatuses { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryMovement> InventoryMovements { get; set; }
    public DbSet<InventoryMovementDetail> InventoryMovementDetails { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Product> Products { get; set; }
    public IQueryable<T> Query<T>() where T : class
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brands_pkey");

            entity.ToTable("brands", "retail");

            entity.HasIndex(e => e.Name, "brands_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasColumnType("bit(1)")
                .HasColumnName("active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Visible)
                .HasColumnType("bit(1)")
                .HasColumnName("visible");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories", "retail");

            entity.HasIndex(e => e.Name, "categories_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("categories_parent_id_fkey");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("documents_pkey");

            entity.ToTable("documents", "retail");

            entity.HasIndex(e => e.DocumentNumber, "documents_document_number_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(50)
                .HasColumnName("document_number");
            entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Total)
                .HasPrecision(12, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.Documents)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("documents_document_type_id_fkey");

            // entity.HasOne(d => d.Person).WithMany(p => p.Documents)
            //     .HasForeignKey(d => d.PersonId)
            //     .OnDelete(DeleteBehavior.SetNull)
            //     .HasConstraintName("documents_person_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Documents)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("documents_status_id_fkey");
        });

        modelBuilder.Entity<DocumentDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_details_pkey");

            entity.ToTable("document_details", "retail");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.Price)
                .HasPrecision(12, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Total)
                .HasPrecision(12, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentDetails)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("document_details_document_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.DocumentDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("document_details_product_id_fkey");
        });

        modelBuilder.Entity<DocumentStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_status_pkey");

            entity.ToTable("document_status", "retail");

            entity.HasIndex(e => e.Name, "document_status_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Visible)
                .HasDefaultValue(true)
                .HasColumnName("visible");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_type_pkey");

            entity.ToTable("document_type", "retail");

            entity.HasIndex(e => e.Name, "document_type_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Visible)
                .HasDefaultValue(true)
                .HasColumnName("visible");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inventory_pkey");

            entity.ToTable("inventory", "retail");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.MaxStock)
                .HasDefaultValue(0)
                .HasColumnName("max_stock");
            entity.Property(e => e.MinStock)
                .HasDefaultValue(0)
                .HasColumnName("min_stock");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReservedQuantity)
                .HasDefaultValue(0)
                .HasColumnName("reserved_quantity");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Location).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("inventory_location_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("inventory_product_id_fkey");
        });

        modelBuilder.Entity<InventoryMovement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inventory_movements_pkey");

            entity.ToTable("inventory_movements", "retail");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DestinationLocationId).HasColumnName("destination_location_id");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");
            entity.Property(e => e.SourceLocationId).HasColumnName("source_location_id");

            entity.HasOne(d => d.DestinationLocation).WithMany(p => p.InventoryMovementDestinationLocations)
                .HasForeignKey(d => d.DestinationLocationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("inventory_movements_destination_location_id_fkey");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.InventoryMovements)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("inventory_movements_document_type_id_fkey");

            entity.HasOne(d => d.SourceLocation).WithMany(p => p.InventoryMovementSourceLocations)
                .HasForeignKey(d => d.SourceLocationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("inventory_movements_source_location_id_fkey");
        });

        modelBuilder.Entity<InventoryMovementDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inventory_movement_details_pkey");

            entity.ToTable("inventory_movement_details", "retail");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.MovementId).HasColumnName("movement_id");
            entity.Property(e => e.MovementType)
                .HasMaxLength(10)
                .HasColumnName("movement_type");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitCost)
                .HasPrecision(12, 2)
                .HasColumnName("unit_cost");

            entity.HasOne(d => d.Movement).WithMany(p => p.InventoryMovementDetails)
                .HasForeignKey(d => d.MovementId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("inventory_movement_details_movement_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.InventoryMovementDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("inventory_movement_details_product_id_fkey");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("locations_pkey");

            entity.ToTable("locations", "retail");

            entity.HasIndex(e => e.Name, "locations_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });
        
         modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products", "retail");

            entity.HasIndex(e => e.Barcode, "products_barcode_key").IsUnique();

            entity.HasIndex(e => e.Sku, "products_sku_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Barcode)
                .HasMaxLength(50)
                .HasColumnName("barcode");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Cost)
                .HasPrecision(12, 2)
                .HasColumnName("cost");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(12, 2)
                .HasColumnName("price");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .HasColumnName("sku");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("products_brand_id_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("products_category_id_fkey");
        });
         
    }
}