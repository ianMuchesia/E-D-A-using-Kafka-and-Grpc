


namespace OrderProcessing.InventoryService.Domain.Entities;


public class InventoryItem
{

    public Guid InventoryItemId { get; set; }
    public Guid ProductId { get; set; }

    public Guid WarehouseId { get; set; }


    public int AvailableQuantity { get; set; }


    public int ReservedQuantity { get; set; }

    public DateTime LastUpdated { get; set; }
}