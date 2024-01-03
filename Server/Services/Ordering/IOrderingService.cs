using Eshop.Shared.Models.Order;

namespace Eshop.Server.Services.Ordering;

public interface IOrderingService
{
    Task<ServiceResponse<bool>> PlaceOrder();
    Task<ServiceResponse<List<OrderDto>>> GetOrders();
    Task<ServiceResponse<OrderDetailDto>> GetOrderDetails(int orderId);
}