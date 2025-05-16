namespace OrderProcessing.InventoryService.Domain.Enums;



public enum ReservationResult
{
    Success,
    Failure,
    InsufficientStock,
    ProductNotFound,
    ReservationAlreadyExists
}