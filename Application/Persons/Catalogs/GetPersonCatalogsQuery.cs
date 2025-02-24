using System;
using Application.Abstractions.Messaging;

namespace Application.Persons.Catalogs;

public record GetPersonCatalogsQuery(string Catalog): IQuery<Dictionary<string, CatalogResponse>>;
