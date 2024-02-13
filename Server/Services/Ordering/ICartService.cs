using Ecommerce.Shared.Orders;
using Ecommerce.Shared;

namespace Ecommerce.Server.Services.Ordering
{
	public interface ICartService
	{
		Task<ServiceResponse<List<CartDTO>>> GetCartProducts(List<CartItem> cartItems);
		Task<ServiceResponse<List<CartDTO>>> StoreCartItems(List<CartItem> cartItems);
		Task<ServiceResponse<int>> GetCartItemsCount();
		Task<ServiceResponse<List<CartDTO>>> GetDbCartProducts(int? userId = null);
		Task<ServiceResponse<bool>> AddToCart(CartItem cartItem);
		Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId);
		Task<ServiceResponse<bool>> UpdateQauntity(CartItem cartItem);
	}
}
