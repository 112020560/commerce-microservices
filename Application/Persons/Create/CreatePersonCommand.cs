using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Persons.Create;

public record class CreatePersonCommand(CreatePersonRequest PersonRequest): ICommand<ResponseObject>;