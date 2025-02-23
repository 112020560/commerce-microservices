using System;

namespace Application.Abstractions.Data;

public interface IUnitOfWork: IDisposable
{
    Task<int> SaveChangesAsync();
}
