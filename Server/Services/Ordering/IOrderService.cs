using Ecommerce.Shared.Orders;
using Ecommerce.Shared;

namespace Ecommerce.Server.Services.Ordering
{
	public interface IOrderService
	{
		Task<ServiceResponse<bool>> PlaceOrder(int userId);
		Task<ServiceResponse<List<OrderDTO>>> GetOrders();
		Task<ServiceResponse<OrderDetailDTO>> GetOrderDetails(int orderId);
	}
}
