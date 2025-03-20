using Inventories.Application.Abstractions.Messaging;
using SharedKernel;

namespace Inventories.Application.Products.Create;

public record class CreateProductsCommand(CreateProductRequest CreateProduct): ICommand<ResponseObject>;