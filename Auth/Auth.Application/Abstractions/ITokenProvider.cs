using System;
using Domain.Models.Auth;

namespace Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
