using Grpc.Core;
using Microsoft.Extensions.Logging;
using OrderProcessing.Inventory.Grpc;
using OrderProcessing.InventoryService.Application.Interfaces;

namespace OrderProcessing.InventoryService.API.Services;

public class InventoryGrpcService : InventoryService.InventoryServiceBase
{
    private readonly IInventoryService _inventoryService;
    private readonly ILogger<InventoryGrpcService> _logger;

    public InventoryGrpcService(IInventoryService inventoryService, ILogger<InventoryGrpcService> logger)
    {
        _inventoryService = inventoryService;
        _logger = logger;
    }

    public override async Task<CheckInventoryResponse> CheckInventory(CheckInventoryRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"[CheckInventory] OrderId: {request.OrderId}");

        // Simulate checking inventory - in reality you'd have logic here
      
      var reservation = await _inventoryService.GetReservationStatusAsync(Guid.Parse(request.OrderId));

        if (reservation == null)
        {
            return new CheckInventoryResponse
            {
                OrderId = request.OrderId,
                Status = CheckInventoryResponse.InventoryStatus.Unavailable
            };
        }

        // Example: Pretend all items are available except one
        foreach (var item in request.Items)
        {
            // Mock condition: If quantity > 5, mark as unavailable
            if (item.Quantity > 5)
            {
                unavailableItems.Add(new CheckInventoryResponse.UnavailableItem
                {
                    ProductId = item.ProductId,
                    RequestedQuantity = item.Quantity,
                    AvailableQuantity = 5 // Mock available quantity
                });
            }
        }

        var status = unavailableItems.Count switch
        {
            0 => CheckInventoryResponse.InventoryStatus.Available,
            var count when count < request.Items.Count => CheckInventoryResponse.InventoryStatus.Partial,
            _ => CheckInventoryResponse.InventoryStatus.Unavailable
        };

        return new CheckInventoryResponse
        {
            OrderId = request.OrderId,
            Status = status,
            UnavailableItems = { unavailableItems }
        };
    }

    public override async Task<ReserveInventoryResponse> ReserveInventory(ReserveInventoryRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"[ReserveInventory] OrderId: {request.OrderId}");

        var items = request.Items
            .Select(i => (Guid.Parse(i.ProductId), i.Quantity))
            .ToList();

        var result = await _inventoryService.ReserveStockAsync(Guid.Parse(request.OrderId), items);

        var response = new ReserveInventoryResponse
        {
            OrderId = request.OrderId,
            ReservationId = result.ReservationId.ToString(),
            Status = result switch
            {
                Domain.Enums.ReservationStatus.Success => ReserveInventoryResponse.ReservationStatus.Success,
                Domain.Enums.ReservationStatus.Partial => ReserveInventoryResponse.ReservationStatus.Partial,
                Domain.Enums.ReservationStatus.Failed => ReserveInventoryResponse.ReservationStatus.Failed,
                _ => ReserveInventoryResponse.ReservationStatus.Failed
            },
            ReservationExpirationMs = (long)result.Expiration.TotalMilliseconds
        };

        response.ReservedItems.AddRange(result.Items.Select(i => new ReserveInventoryResponse.ReservedItem
        {
            ProductId = i.ProductId.ToString(),
            Quantity = i.Quantity,
            WarehouseId = i.WarehouseId.ToString()
        }));

        return response;
    }

    public override async Task<ReleaseInventoryResponse> ReleaseInventory(ReleaseInventoryRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"[ReleaseInventory] ReservationId: {request.ReservationId}, OrderId: {request.OrderId}");

        var success = await _inventoryService.CancelReservationAsync(Guid.Parse(request.OrderId));

        return new ReleaseInventoryResponse
        {
            Success = success,
            Message = success ? "Inventory released successfully" : "Failed to release inventory"
        };
    }
}
