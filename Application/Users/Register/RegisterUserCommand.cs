using System;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Users.Register;

public sealed record RegisterUserCommand(
    string UserName, 
    string Password, 
    string FullName,
    string? SystemUser,
    int[]? Roles
    ): ICommand;
