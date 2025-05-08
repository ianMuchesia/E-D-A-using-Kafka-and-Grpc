using Grpc.Core;
using OrderProcessing.Inventory.Grpc;

namespace OrderProcessing.InventoryService.API.Services
{
    public class InventoryGrpcService : Grpc.InventoryService.InventoryServiceBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<InventoryGrpcService> _logger;

        public InventoryGrpcService(IInventoryService inventoryService, ILogger<InventoryGrpcService> logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }

        // gRPC methods (e.g., CheckInventory, ReserveInventory)
        public override async Task<CheckInventoryResponse> CheckInventory(CheckInventoryRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Checking inventory for order {request.OrderId}");

            // Your logic to check inventory and return response
        }

        public override async Task<ReserveInventoryResponse> ReserveInventory(ReserveInventoryRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Reserving inventory for order {request.OrderId}");

            // Your logic to reserve inventory and return response
        }
    }
}
