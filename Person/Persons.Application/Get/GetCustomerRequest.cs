namespace Persons.Application.Get;

public record  GetCustomerRequest(Guid? Id, int Type, Guid? Status, string? Name, int PageSize, int PageNumber);
