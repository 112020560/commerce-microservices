using Application.Abstractions.Data;
using Domain.Models.Auth;

namespace Application.Auth.Data.Auth;

public interface IAuthUnitOfWork
{
    public IRepository<User> UserRepository { get; }
}
