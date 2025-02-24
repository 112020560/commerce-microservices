using System;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Persons.Get;

public record GetAllPersonsQuery(GetCustomerRequest Request): IQuery<ResponseObject<List<CustomPersonResponse>>>;
