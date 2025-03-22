using Auth.Domain.Entities;

namespace Auth.Application.Abstractions;

public interface ITokenProvider
{
    string Create(AuthUser user);
}
