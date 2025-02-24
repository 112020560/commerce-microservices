using System;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Persons.GetAll;

public record GetAllCustomerQuery(): IQuery<ResponseObject<CustomerResponse>>;
