using Grpc.Net.Client;
using OrderProcessing.Inventory.Grpc;
using OrderProcessing.InventoryService.Application.Interfaces;
// This comes from the proto-generated C# code

namespace OrderProcessing.InventoryService.API.Services
{
    public class ProductGrpcClient : IProductGrpcClient
    {
        private readonly ProductService.ProductServiceClient _client;
        private readonly ILogger<ProductGrpcClient> _logger;

        public ProductGrpcClient(IConfiguration configuration, ILogger<ProductGrpcClient> logger)
        {
            var channel = GrpcChannel.ForAddress(configuration["GrpcServices:Product"]);
            _client = new ProductService.ProductServiceClient(channel);
            _logger = logger;
        }

        public async Task<GetProductResponse> GetProductAsync(Guid productId)
        {
            _logger.LogInformation($"Calling ProductService.GetProduct for ProductId: {productId}");

            var request = new GetProductRequest { ProductId = productId.ToString() };
            return await _client.GetProductAsync(request);
        }

        public async Task<UpdateProductStockResponse> UpdateProductStockAsync(Guid productId, int quantityChange)
        {
            _logger.LogInformation($"Calling ProductService.UpdateProductStock for ProductId: {productId}, QuantityChange: {quantityChange}");

            var request = new UpdateProductStockRequest
            {
                ProductId = productId.ToString(),
                QuantityChange = quantityChange
            };
            return await _client.UpdateProductStockAsync(request);
        }
    }
}
