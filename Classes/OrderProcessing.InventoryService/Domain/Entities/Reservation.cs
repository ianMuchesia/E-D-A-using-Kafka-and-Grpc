namespace OrderProcessing.InventoryService.Domain.Entities;

using Domain.Enums;


public class Reservation
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Domain.Enums.ReservationStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    
    private Reservation() { } // For EF Core
    
    public Reservation(Guid orderId, Guid productId, int quantity)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Status = ReservationStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void Complete()
    {
        Status = ReservationStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }
    
    public void Cancel()
    {
        Status = ReservationStatus.Cancelled;
        CompletedAt = DateTime.UtcNow;
    }
}

