using Application.Abstractions.Data;
using Domain.Models.Auth;
using Domain.Models.crm;
using Domain.Models.retail;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public partial class CommerceDbContext : DbContext, IApplicationDbContext
{

    public CommerceDbContext(DbContextOptions<CommerceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get;}

    public virtual DbSet<AddressType> AddressTypes { get;}

    public virtual DbSet<Domain.Models.crm.Attribute> Attributes { get;}

    public virtual DbSet<AttributesDataType> AttributesDataTypes { get;}

    public virtual DbSet<Brand> Brands { get;}

    public virtual DbSet<Category> Categories { get; }

    public virtual DbSet<Document> Documents { get;  }

    public virtual DbSet<DocumentDetail> DocumentDetails { get;  }

    public virtual DbSet<DocumentStatus> DocumentStatuses { get;}

    public virtual DbSet<DocumentType> DocumentTypes { get; }

    public virtual DbSet<Inventory> Inventories { get; }

    public virtual DbSet<InventoryMovement> InventoryMovements { get;  }

    public virtual DbSet<InventoryMovementDetail> InventoryMovementDetails { get; }

    public virtual DbSet<Location> Locations { get; }

    public virtual DbSet<MenuOption> MenuOptions { get;}

    public virtual DbSet<Module> Modules { get;  }

    public DbSet<Person> Person { get; set;}

    public virtual DbSet<PersonRelationship> PersonRelationships { get;  }

    public virtual DbSet<PersonType> PersonTypes { get;  }

    public virtual DbSet<Product> Products { get; }

    public virtual DbSet<RelationshipType> RelationshipTypes { get;  }

    public virtual DbSet<Role> Roles { get; }

    public virtual DbSet<RoleModule> RoleModules { get;  }

    public virtual DbSet<User> Users { get;  }

    public virtual DbSet<UserRole> UserRoles { get;  }

    public IQueryable<T> Query<T>() where T : class
    {
        return Set<T>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("address_pkey");

            entity.ToTable("address", "crm");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Address1).HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .HasColumnName("postal_code");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.Person).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("address_person_id_fkey");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("address_type_fkey");
        });

        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("address_type_pkey");

            entity.ToTable("address_type", "crm");

            entity.HasIndex(e => e.Name, "address_type_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Visible).HasColumnName("visible");
        });

        modelBuilder.Entity<Domain.Models.crm.Attribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("attributes_pkey");

            entity.ToTable("attributes", "crm");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.DataType).HasColumnName("data_type");
            entity.Property(e => e.Key)
                .HasMaxLength(50)
                .HasColumnName("key");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.DataTypeNavigation).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.DataType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("attributes_data_type_fkey");

            entity.HasOne(d => d.Person).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("attributes_person_id_fkey");
        });

        modelBuilder.Entity<AttributesDataType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("attributes_type_pkey");

            entity.ToTable("attributes_data_type", "crm");

            entity.HasIndex(e => e.Name, "attributes_type_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('crm.attributes_type_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Visible).HasColumnName("visible");
        });

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

            entity.HasOne(d => d.Person).WithMany(p => p.Documents)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("documents_person_id_fkey");

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

        modelBuilder.Entity<MenuOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("menu_option_pk");

            entity.ToTable("menu_option", "auth");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .HasColumnName("icon");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .HasColumnName("url");
            entity.Property(e => e.Visible).HasColumnName("visible");

            entity.HasOne(d => d.Module).WithMany(p => p.MenuOptions)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menu_option_module_id_fk");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("module_pk");

            entity.ToTable("module", "auth");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsEnable)
                .HasDefaultValue(true)
                .HasColumnName("is_enable");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mtr_person_pk");

            entity.ToTable("person", "crm");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BirthDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birth_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.Email)
                .HasMaxLength(225)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(150)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.UpdateAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<PersonRelationship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("person_relationships_pkey");

            entity.ToTable("person_relationships", "crm");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.PersonSource).HasColumnName("person_source");
            entity.Property(e => e.PersonTarget).HasColumnName("person_target");
            entity.Property(e => e.RelationshipType).HasColumnName("relationship_type");

            entity.HasOne(d => d.PersonSourceNavigation).WithMany(p => p.PersonRelationshipPersonSourceNavigations)
                .HasForeignKey(d => d.PersonSource)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("person_relationships_person_source_fkey");

            entity.HasOne(d => d.PersonTargetNavigation).WithMany(p => p.PersonRelationshipPersonTargetNavigations)
                .HasForeignKey(d => d.PersonTarget)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("person_relationships_person_target_fkey");

            entity.HasOne(d => d.RelationshipTypeNavigation).WithMany(p => p.PersonRelationships)
                .HasForeignKey(d => d.RelationshipType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_relationships_relationship_type_fkey");
        });

        modelBuilder.Entity<PersonType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cat_person_type_pk");

            entity.ToTable("person_type", "crm");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_at");
            entity.Property(e => e.Visible).HasColumnName("visible");
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

        modelBuilder.Entity<RelationshipType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("relationship_type_pkey");

            entity.ToTable("relationship_type", "crm");

            entity.HasIndex(e => e.Name, "relationship_type_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Visible).HasColumnName("visible");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pk");

            entity.ToTable("role", "auth");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsEnable)
                .HasDefaultValue(true)
                .HasColumnName("is_enable");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasMany(d => d.MenuOptions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleMenuOption",
                    r => r.HasOne<MenuOption>().WithMany()
                        .HasForeignKey("MenuOptionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("role_menu_option_menu_option_id_fk"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("role_menu_option_role_id_fk"),
                    j =>
                    {
                        j.HasKey("RoleId", "MenuOptionId").HasName("role_menu_option_pk");
                        j.ToTable("role_menu_option", "auth");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<int>("MenuOptionId").HasColumnName("menu_option_id");
                    });
        });

        modelBuilder.Entity<RoleModule>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.ModuleId }).HasName("role_module_pk");

            entity.ToTable("role_module", "auth");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.CanDelete).HasColumnName("can_delete");
            entity.Property(e => e.CanRead).HasColumnName("can_read");
            entity.Property(e => e.CanUpdate).HasColumnName("can_update");
            entity.Property(e => e.CanWrite).HasColumnName("can_write");

            entity.HasOne(d => d.Module).WithMany(p => p.RoleModules)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_module_module_id_fk");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleModules)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_module_role_id_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");

            entity.ToTable("user", "auth");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.FullName)
                .HasMaxLength(200)
                .HasColumnName("full_name");
            entity.Property(e => e.IsEnable).HasColumnName("is_enable");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .HasColumnName("password");
            entity.Property(e => e.UpdateAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasComment("username of a system")
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("user_role_pk");

            entity.ToTable("user_role", "auth");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.AssignmentDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("assignment_date");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_role_id_fk");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_user_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
