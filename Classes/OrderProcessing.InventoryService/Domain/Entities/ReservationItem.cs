


namespace OrderProcessing.InventoryService.Domain.Entities;


public class ReservationItem
{

  public Guid Id { get; private set; }
    public Guid ReservationId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public Reservation Reservation { get; private set; } // Navigation property
    
    private ReservationItem() { } // For EF Core
    
    public ReservationItem(Guid reservationId, Guid productId, int quantity)
    {
        Id = Guid.NewGuid();
        ReservationId = reservationId;
        ProductId = productId;
        Quantity = quantity;
    }
}