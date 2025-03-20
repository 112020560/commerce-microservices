using Application.Persons.Create;
using Persons.Application.Abstractions.Messaging;
using SharedKernel;

namespace Persons.Application.Persons.Create;

public record class CreatePersonCommand(CreatePersonRequest PersonRequest): ICommand<ResponseObject>;