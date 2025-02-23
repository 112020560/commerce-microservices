using System;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Customers.Create;

public record CreateCustomerCommand():ICommand<ResponseObject>;
