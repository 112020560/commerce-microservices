using Inventories.Application.Abstractions.Data;
using Inventories.Application.Abstractions.Messaging;
using Inventories.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Inventories.Application.Products.Create;

internal sealed class CreateProductsCommandHandler : ICommandHandler<CreateProductsCommand, ResponseObject>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    public CreateProductsCommandHandler(IApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<ResponseObject>> Handle(CreateProductsCommand request, CancellationToken cancellationToken)
    {
        var existProduct = await _dbContext.Query<Product>().AsNoTracking().FirstOrDefaultAsync(x => x.Sku == request.CreateProduct.Sku, cancellationToken: cancellationToken);
        if (existProduct is null)
        {
            //return new Result<ResponseObject>(new ResponseObject { IsSuccess= false, Message = "" }, false, Error.None);
            existProduct = new Product
            {
                Sku = request.CreateProduct.Sku,
                Name = request.CreateProduct.Name,
                Description = request.CreateProduct.Description,
                Price = request.CreateProduct.Price,
                CreatedAt = _dateTimeProvider.UtcNow,
                UpdatedAt = _dateTimeProvider.UtcNow,
                Barcode = request.CreateProduct.Barcode,
                CategoryId = request.CreateProduct.CategoryId,
                //Image = request.CreateProduct.Image,
                //Unit = request.CreateProduct.Unit,
                BrandId = request.CreateProduct.BrandId,
                Cost = request.CreateProduct.Cost,
            };

            await _dbContext.Products.AddAsync(existProduct, cancellationToken);
        }
        else
        {
            existProduct.Name = request.CreateProduct.Name;
            existProduct.Description = request.CreateProduct.Description;
            existProduct.Price = request.CreateProduct.Price;
            existProduct.UpdatedAt = _dateTimeProvider.UtcNow;
            existProduct.Barcode = request.CreateProduct.Barcode;
            existProduct.CategoryId = request.CreateProduct.CategoryId;
            //existProduct.Image = request.CreateProduct.Image;
            //existProduct.Unit = request.CreateProduct.Unit;
            existProduct.BrandId = request.CreateProduct.BrandId;
            existProduct.Cost = request.CreateProduct.Cost;
            _dbContext.Products.Update(existProduct);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Result<ResponseObject>(new ResponseObject { IsSuccess = true, Message = "Product created successfully" }, true, Error.None);
    }
}
