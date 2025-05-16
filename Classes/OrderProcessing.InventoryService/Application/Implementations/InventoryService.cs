



using Avro;
using OrderProcessing.InventoryService.Application.Interfaces;
using OrderProcessing.InventoryService.Domain.Entities;

namespace OrderProcessing.InventoryService.Application.Implementations;


public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(
        IInventoryRepository inventoryRepository,
    
        ILogger<InventoryService> logger)
    {
        _inventoryRepository = inventoryRepository;
       
        _logger = logger;
    }

    public async Task<bool> CancelReservationAsync(Guid orderId)
    {
        var reservation = await _inventoryRepository.GetByOrderIdAsync(orderId);
        if (reservation == null)
        {
            _logger.LogWarning($"Reservation with Order ID {orderId} not found.");
            return false;
        }

        reservation.Cancel();
         await _inventoryRepository.UpdateReservationAsync(reservation);

        var result = await _inventoryRepository.ReleaseItemsAsync(reservation.Id);
        if (!result)
        {
            _logger.LogError($"Failed to release items for reservation with Order ID {orderId}.");
            return false;
        }

        return true;
    }

    public async Task<bool> CompleteReservationAsync(Guid orderId)
    {
        var reservation = await _inventoryRepository.GetByOrderIdAsync(orderId);
        if (reservation == null)
        {
            _logger.LogWarning($"Reservation with Order ID {orderId} not found.");
            return false;
        }

        reservation.Complete();
         await _inventoryRepository.UpdateReservationAsync(reservation);

        // var result = await _inventoryRepository.ReleaseItemsAsync(reservation.Id);
        // if (!result)
        // {
        //     _logger.LogError($"Failed to release items for reservation with Order ID {orderId}.");
        //     return false;
        // }

        return true;
    }

    public async Task<Domain.Enums.ReservationStatus> GetReservationStatusAsync(Guid orderId)
    {
        var reservation = await _inventoryRepository.GetByOrderIdAsync(orderId);
        if (reservation == null)
        {
            _logger.LogWarning($"Reservation with Order ID {orderId} not found.");
            return Domain.Enums.ReservationStatus.Pending;
        }

        return reservation.Status;
    }

    public async Task<Domain.Enums.ReservationResult> ReserveStockAsync(Guid orderId, List<(Guid ProductId, int Quantity)> items)
    {
        var reservation = new Domain.Entities.Reservation(orderId, items.Count);
        var result = await _inventoryRepository.AddReservationAsync(reservation);
        if (!result)
        {
            _logger.LogError($"Failed to add reservation for Order ID {orderId}.");
            return Domain.Enums.ReservationResult.Failure;
        }

        foreach (var item in items)
        {
            var reservationItem = new ReservationItem(reservation.Id, item.ProductId, item.Quantity);
            var itemResult = await _inventoryRepository.AddItemAsync(reservationItem);
            if (!itemResult)
            {
                _logger.LogError($"Failed to add item to reservation for Order ID {orderId}.");
                return Domain.Enums.ReservationResult.Failure;
            }
        }

        return Domain.Enums.ReservationResult.Success;
    }
}


    