using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure;

public partial class AuthDbContext : DbContext
{
    public AuthDbContext()
    {
    }

    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthMenuOption> AuthMenuOptions { get; set; }

    public virtual DbSet<AuthModule> AuthModules { get; set; }

    public virtual DbSet<AuthRole> AuthRoles { get; set; }

    public virtual DbSet<AuthRoleModule> AuthRoleModules { get; set; }

    public virtual DbSet<AuthUser> AuthUsers { get; set; }

    public virtual DbSet<AuthUserRole> AuthUserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=access_control;Username=root;Password=root;Include Error Detail=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthMenuOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("menu_option_pk");

            entity.ToTable("auth_menu_option");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
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

            entity.HasOne(d => d.Module).WithMany(p => p.AuthMenuOptions)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menu_option_module_id_fk");
        });

        modelBuilder.Entity<AuthModule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auth_module_pk");

            entity.ToTable("auth_module");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
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

        modelBuilder.Entity<AuthRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auth_role_pk");

            entity.ToTable("auth_role");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
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
                    "AuthRoleMenuOption",
                    r => r.HasOne<AuthMenuOption>().WithMany()
                        .HasForeignKey("MenuOptionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("role_menu_option_menu_option_id_fk"),
                    l => l.HasOne<AuthRole>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("role_menu_option_role_id_fk"),
                    j =>
                    {
                        j.HasKey("RoleId", "MenuOptionId").HasName("role_menu_option_pk");
                        j.ToTable("auth_role_menu_option");
                        j.IndexerProperty<Guid>("RoleId")
                            .HasDefaultValueSql("gen_random_uuid()")
                            .HasColumnName("role_id");
                        j.IndexerProperty<Guid>("MenuOptionId").HasColumnName("menu_option_id");
                    });
        });

        modelBuilder.Entity<AuthRoleModule>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.ModuleId }).HasName("auth_role_module_pk");

            entity.ToTable("auth_role_module");

            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("role_id");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.CanDelete).HasColumnName("can_delete");
            entity.Property(e => e.CanRead).HasColumnName("can_read");
            entity.Property(e => e.CanUpdate).HasColumnName("can_update");
            entity.Property(e => e.CanWrite).HasColumnName("can_write");

            entity.HasOne(d => d.Module).WithMany(p => p.AuthRoleModules)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_module_module_id_fk");

            entity.HasOne(d => d.Role).WithMany(p => p.AuthRoleModules)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_module_role_id_fk");
        });

        modelBuilder.Entity<AuthUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auth_user_pk");

            entity.ToTable("auth_user");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.IsEnable).HasColumnName("is_enable");
            entity.Property(e => e.Name).HasColumnName("name");
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
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<AuthUserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("auth_user_role_pk");

            entity.ToTable("auth_user_role");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.AssigmentDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("assigment_date");

            entity.HasOne(d => d.Role).WithMany(p => p.AuthUserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_user_role_auth_role_id_fk");

            entity.HasOne(d => d.User).WithMany(p => p.AuthUserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_user_role_auth_user_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
