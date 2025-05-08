



namespace OrderProcessing.PaymentService.Domain.Entities
{
    public class Payment
    {
        public Guid PaymentId { get; set; }

        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public decimal Amount { get; set; }


        public PaymentStatus Status { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public string? PaymentMethod { get; set; } // e.g., Credit Card, PayPal, etc.


        public string? TransactionId { get; set; } // Unique identifier from the payment processor
        // Additional properties and methods can be added here as needed
    }
}