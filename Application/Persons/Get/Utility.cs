using System;
using System.Linq.Expressions;
using Domain.Models.crm;
using LinqKit;

namespace Application.Persons.Get;

public abstract class Utility
{
    protected static Expression<Func<Person, bool>> BuildFilter(GetCustomerRequest request)
    {
        var predicate = PredicateBuilder.New<Person>(true); // Comienza con "true" para permitir AND dinámicos.

        if (request.Id.HasValue)
        {
            predicate = predicate.And(p => p.Id == request.Id.Value);
        }

        if (request.Status.HasValue)
        {
            predicate = predicate.And(p => p.StatusId == request.Status.Value);
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            predicate = predicate.And(p => string.Concat(p.FirstName, " ", p.LastName).Contains(request.Name));
        }

        if (request.Type > 0)  // Suponiendo que 0 no es un tipo válido
        {
            predicate = predicate.And(p => p.TypeId == request.Type);
        }

        return predicate;
    }
    protected List<CustomPersonResponse>? ParseEntityToResponse(List<Person>? entities)
    {
        if (entities is not null && entities.Count > 0)
        {
            var personResponse = entities.Select(e => new CustomPersonResponse
            {
                BirthDate = e.BirthDate,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Id = e.Id,
                Phone = e.Phone,
                Email = e.Email,
                PersonAddresses = [.. e.Addresses.Select(a => new RespPersonAddress {
                    AddressDetail = a.Address1,
                    City = a.City ?? "N/D",
                    Country = a.Country?? "N/D",
                     Type = a.Type,
                      ZipCode = a.PostalCode?? "N/D",
                    State = a.State?? "N/D"
                 })],
                PersonAttributtes = [.. e.Attributes.Select(a => new RespPersonAttributte {
                    Key = a.Key,
                    Value = a.Value,
                    DataType = a.DataType
                 })]
            }).ToList();

            return personResponse;
        }
        else{
            return [];
        }
    }
}
