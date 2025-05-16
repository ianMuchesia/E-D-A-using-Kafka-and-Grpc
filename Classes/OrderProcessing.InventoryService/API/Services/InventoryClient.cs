// using Grpc.Net.Client;

// namespace OrderProcessing.InventoryService.API.Services
// {
//     public class InventoryClient : IInventoryClient
//     {
//         private readonly Grpc.InventoryService.InventoryServiceClient _client;
//         private readonly ILogger<InventoryClient> _logger;

//         public InventoryClient(IConfiguration configuration, ILogger<InventoryClient> logger)
//         {
//             var channel = GrpcChannel.ForAddress(configuration["GrpcServices:Inventory"]);
//             _client = new Grpc.InventoryService.InventoryServiceClient(channel);
//             _logger = logger;
//         }

//         public async Task<InventoryCheckResult> CheckInventoryAsync(string orderId, List<OrderItem> items)
//         {
//             // Your logic to call the CheckInventory method on the gRPC server
//         }

//         public async Task<InventoryReservationResult> ReserveInventoryAsync(string orderId, List<OrderItem> items, TimeSpan expirationTime)
//         {
//             // Your logic to call the ReserveInventory method on the gRPC server
//         }
//     }
// }
