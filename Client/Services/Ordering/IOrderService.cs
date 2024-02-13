using Ecommerce.Shared.Orders;

namespace Ecommerce.Client.Services.Ordering
{
	public interface IOrderService
	{
		Task<string> PlaceOrder();
		Task<List<OrderDTO>> GetOrders();
		Task<OrderDetailDTO> GetOrderDetails(int orderId);
	}
}
