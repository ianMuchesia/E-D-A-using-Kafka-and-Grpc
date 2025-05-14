

using Grpc.Core;
using OrderProcessing.Product.Grpc;
using OrderProcessing.ProductService.Application.Interfaces;

namespace OrderProcessing.ProductService.API.Services;


public class ProductGrpcService : OrderProcessing.Product.Grpc.ProductService.ProductServiceBase
{
    private readonly IProductRepository _productRepository;

    public ProductGrpcService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public override async Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
    {
        var product = await _productRepository.GetProductByIdAsync(Guid.Parse(request.ProductId));
        if (product == null)
            throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));

        return new GetProductResponse
        {
            ProductId = product.Id.ToString(),
            Name = product.Name,
            Price = (double)product.Price,
            Stock = product.Stock
        };
    }

    public override async Task<UpdateProductStockResponse> UpdateProductStock(UpdateProductStockRequest request, ServerCallContext context)
    {
        var product = await _productRepository.GetProductByIdAsync(Guid.Parse(request.ProductId));
        if (product == null)
            return new UpdateProductStockResponse { Success = false, Message = "Product not found" };

        try
        {
            product.UpdateStock(request.QuantityChange);
            await _productRepository.UpdateProductAsync(product);

            return new UpdateProductStockResponse { Success = true, Message = "Stock updated successfully" };
        }
        catch (Exception ex)
        {
            return new UpdateProductStockResponse { Success = false, Message = ex.Message };
        }
    }
}
