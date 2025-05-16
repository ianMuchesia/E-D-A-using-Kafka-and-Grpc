


using OrderProcessing.InventoryService.Domain.Enums;

namespace OrderProcessing.InventoryService.Application.Interfaces;

public interface IInventoryService
{
     Task<ReservationResult> ReserveStockAsync(Guid orderId, List<(Guid ProductId, int Quantity)> items);
    Task<bool> CompleteReservationAsync(Guid orderId);
    Task<bool> CancelReservationAsync(Guid orderId);
    Task<ReservationStatus> GetReservationStatusAsync(Guid orderId);

}