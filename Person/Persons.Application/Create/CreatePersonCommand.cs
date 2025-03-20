using Persons.Application.Abstractions.Messaging;
using SharedKernel;

namespace Persons.Application.Create;

public record class CreatePersonCommand(CreatePersonRequest PersonRequest): ICommand<ResponseObject>;