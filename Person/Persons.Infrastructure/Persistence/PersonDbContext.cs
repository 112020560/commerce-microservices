using Microsoft.EntityFrameworkCore;
using Persons.Application.Abstractions.Data;

namespace Person.Infrastructure.Persistence;

public class PersonDbContext : DbContext, IApplicationDbContext
{
    
}