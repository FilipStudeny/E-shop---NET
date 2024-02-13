using Ecommerce.Shared.Orders;

namespace Ecommerce.Client.Services.Ordering
{
	public interface ICartService
	{
		event Action OnCartChange;
		Task AddToCart(CartItem item);
		Task<List<CartDTO>> GetCartProducts();
		Task RemoveItemFromCart(int productId, int productTypeId);
		Task UpdateQuantityForProduct(CartDTO item);

		Task StoreCartItemsInDatabase(bool emptyLocalCart);
		Task GetCartItemCount();
	}
}
