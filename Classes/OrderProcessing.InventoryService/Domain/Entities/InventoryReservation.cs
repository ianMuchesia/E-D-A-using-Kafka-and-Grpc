namespace OrderProcessing.InventoryService.Domain.Entities;


public class InventoryReservation
{
    public Guid ReservationId { get; set; }

    public Guid OrderId { get; set; }

   public IList<ReservedItem> ReservedItems { get; set; } = new List<ReservedItem>();

   public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddHours(1);
}