using System;
using Domain.Models.Auth;
using Domain.Models.crm;
using Domain.Models.retail;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Address> Addresses { get;  }

    DbSet<AddressType> AddressTypes { get;  }

    DbSet<Domain.Models.crm.Attribute> Attributes { get;  }

    DbSet<AttributesDataType> AttributesDataTypes { get;  }

    DbSet<Brand> Brands { get;  }

    DbSet<Category> Categories { get;  }

    DbSet<Document> Documents { get;  }

    DbSet<DocumentDetail> DocumentDetails { get;  }

    DbSet<DocumentStatus> DocumentStatuses { get;  }

    DbSet<DocumentType> DocumentTypes { get;  }

    DbSet<Inventory> Inventories { get;  }

    DbSet<InventoryMovement> InventoryMovements { get;  }

    DbSet<InventoryMovementDetail> InventoryMovementDetails { get;  }

    DbSet<Location> Locations { get;  }

    DbSet<MenuOption> MenuOptions { get;  }

    DbSet<Module> Modules { get;  }

    DbSet<Person> Person { get; set; }

    DbSet<PersonRelationship> PersonRelationships { get;  }

    DbSet<PersonType> PersonTypes { get;  }

    DbSet<Product> Products { get;  }

    DbSet<RelationshipType> RelationshipTypes { get;  }

    DbSet<Role> Roles { get;  }

    DbSet<RoleModule> RoleModules { get;  }

    DbSet<User> Users { get;  }

    DbSet<UserRole> UserRoles { get;  }

    IQueryable<T> Query<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
