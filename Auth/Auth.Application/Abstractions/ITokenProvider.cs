using Auth.Domain.Entities;

namespace Auth.Application.Abstractions;

public interface ITokenProvider
{
    string Create(User user);
}
