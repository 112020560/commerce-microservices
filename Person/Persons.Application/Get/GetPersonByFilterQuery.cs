using Application.Persons.Get;
using Persons.Application.Abstractions.Messaging;
using SharedKernel;

namespace Persons.Application.Persons.Get;

public record GetPersonByFilterQuery(GetCustomerRequest Request): IQuery<ResponseObject<List<CustomPersonResponse>>>;
