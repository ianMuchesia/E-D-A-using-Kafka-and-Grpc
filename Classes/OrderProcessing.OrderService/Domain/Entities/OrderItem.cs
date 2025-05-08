



namespace OrderProcessing.OrderService.Domain.Entities;


public class OrderItem
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }


    public decimal UnitPrice { get; set; }

    public decimal Subtotal => UnitPrice * Quantity;
}