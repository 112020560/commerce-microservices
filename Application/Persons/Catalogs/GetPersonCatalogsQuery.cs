using System;
using Application.Abstractions.Messaging;

namespace Application.Persons.Catalogs;

public record GetPersonCatalogsQuery(string[] CatalogNames): IQuery<Dictionary<string, List<CatalogResponse>>>;
