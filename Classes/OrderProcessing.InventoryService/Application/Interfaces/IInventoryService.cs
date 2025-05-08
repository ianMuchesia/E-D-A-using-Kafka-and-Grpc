


namespace OrderProcessing.InventoryService.Application.Interfaces;

public interface IInventoryService
{
    Task<InventoryCheckResult> CheckInventoryAsync(string orderId, List<InventoryCheckItem> items);

    Task<InventoryReservationResult> ReserveInventoryAsync(string orderId, List<InventoryReservationItem> items, DateTimeOffset expirationTime);

    Task<ReleaseInventoryResult> ReleaseInventoryAsync(string reservationId, string orderId);


    Task<ProductInventory> GetProductInventoryAsync(string productId);

}