



using OrderProcessing.Inventory.Grpc;

namespace OrderProcessing.InventoryService.Application.Interfaces;

public interface IProductGrpcClient
{
    Task<GetProductResponse> GetProductAsync(Guid productId);
    Task<UpdateProductStockResponse> UpdateProductStockAsync(Guid productId, int quantityChange);
}
