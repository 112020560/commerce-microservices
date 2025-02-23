using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Person.Create;

public record class CreatePersonCommand(CreatePersonRequest PersonRequest): ICommand<ResponseObject>;