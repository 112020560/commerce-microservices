using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Products.Create;

public record class CreateProductsCommand(CreateProductRequest CreateProduct): ICommand<ResponseObject>;