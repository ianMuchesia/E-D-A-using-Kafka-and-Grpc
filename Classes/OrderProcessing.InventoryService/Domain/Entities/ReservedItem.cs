namespace OrderProcessing.InventoryService.Domain.Entities;



public class ReservedItem
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

   public Guid WarehouseId { get; set; }

    
}