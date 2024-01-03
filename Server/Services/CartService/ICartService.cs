using Eshop.Shared.DTOs;
using Eshop.Shared.Models.Cart;

namespace Eshop.Server.Services.CartService;

public interface ICartService
{
    Task<ServiceResponse<List<CartDto>>> GetProductsInCart(List<CartItem> cartItems);
    Task<ServiceResponse<List<CartDto>>> StoreCartItemsInDatabase(List<CartItem> cartItems);
    Task<ServiceResponse<int>> GetCartItemCount();
    Task<ServiceResponse<List<CartDto>>> GetStoredCart();
    Task<ServiceResponse<bool>> AddToCart(CartItem cartItem);

}