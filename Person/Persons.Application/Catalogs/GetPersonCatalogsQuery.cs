using Persons.Application.Abstractions.Messaging;

namespace Persons.Application.Persons.Catalogs;

public record GetPersonCatalogsQuery(string[] CatalogNames): IQuery<Dictionary<string, List<CatalogResponse>>>;
