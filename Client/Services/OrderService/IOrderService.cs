using Eshop.Shared.Models.Order;

namespace Eshop.Client.Services.OrderService;

public interface IOrderService
{
    Task PlaceOrder();
    Task<List<OrderDto>> GetOrders();
    Task<OrderDetailDto> GetOrderDetails(int orderId);

}