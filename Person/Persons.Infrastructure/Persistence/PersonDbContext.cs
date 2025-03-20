using Microsoft.EntityFrameworkCore;
using Persons.Application.Abstractions.Data;
using Persons.Domain.Entities;
using Attribute = Persons.Domain.Entities.Attribute;

namespace Persons.Infrastructure.Persistence;

public partial class PersonDbContext(DbContextOptions<PersonDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<Attribute> Attributes { get; set; }
    public DbSet<AttributesDataType> AttributesDataTypes { get; set; }
    public DbSet<Person> Person { get; set; }
    public DbSet<PersonRelationship> PersonRelationships { get; set; }
    public DbSet<PersonType> PersonTypes { get; set; }
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

        modelBuilder.Entity<Persons.Domain.Entities.Attribute>(entity =>
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
        
        OnModelCreatingPartial(modelBuilder);
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}