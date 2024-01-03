using Eshop.Shared.DTOs;
using Eshop.Shared.Models.Cart;

namespace Eshop.Client.Services.CartService;

public interface ICartService
{
    event Action OnCartChange;
    Task AddToCart(CartItem item);
    Task<List<CartDto>> GetCartProducts();
    Task RemoveItemFromCart(int productId, int productTypeId);
    Task UpdateQuantityForProduct(CartDto item);

    Task StoreCartItemsInDatabase(bool emptyLocalCart);
    Task GetCartItemCount();
    
}