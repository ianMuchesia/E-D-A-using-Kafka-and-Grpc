namespace OrderProcessing.OrderService.Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }


        public DateTime OrderDate { get; set; }


        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal TotalAmount { get; set; }


        public OrderStatus Status { get; set; }


        public string? PaymentId { get; set; }

        public string? ReservationId { get; set; }

        
    }
}