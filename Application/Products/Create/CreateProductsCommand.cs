using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Products;

public record class CreateProductsCommand(CreateProductRequest CreateProduct): ICommand<ResponseObject>;