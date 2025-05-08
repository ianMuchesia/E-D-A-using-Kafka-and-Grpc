namespace OrderProcessing.OrderService.Domain.Entities;


public enum OrderStatus
{
   Created,

   InventoryChecked,

   InventoryReserved,

   PaymentProcessing,


   PaymentCompleted,

   PaymentFailed,

   OutOfStock,


   Cancelled,
}