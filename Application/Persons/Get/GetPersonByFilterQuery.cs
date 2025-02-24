using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Persons.Get;

public record GetPersonByFilterQuery(GetCustomerRequest Request): IQuery<ResponseObject<List<CustomPersonResponse>>>;
