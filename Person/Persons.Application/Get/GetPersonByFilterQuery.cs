using Persons.Application.Abstractions.Messaging;
using SharedKernel;

namespace Persons.Application.Get;

public record GetPersonByFilterQuery(GetCustomerRequest Request): IQuery<ResponseObject<List<CustomPersonResponse>>>;
