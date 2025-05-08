namespace OrderProcessing.OrderService.Application.Interfaces;


public interface IOrderService
{
    Task<Domain.Entities.Order> CreateOrderAsync(OrderCreateCommand order);


    Task<OrderProcessing> GetOrderAsync(Guid orderId);


    Task<bool> UpdateOrderStatusAsync(string orderId, Domain.Entities.OrderStatus status, Dictionary<string, string> additionalData);

    Task<IEnumerable<Domain.Entities.Order>> GetCustomerOrdersAsync(string customerId);
}