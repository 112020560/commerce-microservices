using System;
using Application.Abstractions.Data.Auth;
using Domain.Models.Auth;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories.Auth;

public class UserRepository: Repository<User>,IUserRepository
{
    private readonly CommerceDbContext _dbContext;
    public UserRepository(CommerceDbContext dbContext): base(dbContext)
    {
        _dbContext= dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
}
