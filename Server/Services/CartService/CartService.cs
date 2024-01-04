using Eshop.Server.Database;
using Eshop.Server.Services.Authentication;
using Eshop.Shared.DTOs;
using Eshop.Shared.Models.Cart;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Services.CartService;

public class CartService : ICartService
{
    private readonly DataContext _dataContext;
    private readonly IAuthenticationService _authenticationService;

    public CartService(DataContext dataContext, IAuthenticationService authenticationService)
        {
            _dataContext = dataContext;
            _authenticationService = authenticationService;
        }


        public async Task<ServiceResponse<List<CartDto>>> GetCartProducts(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartDto>>
            {
                Data = new List<CartDto>()
            };

            foreach (var item in cartItems)
            {
                var product = await _dataContext.Products.Where(p => p.Id == item.ProductId).FirstOrDefaultAsync();

                if (product == null)
                {
                    continue;
                }

                var productVariant = await _dataContext.ProductVariants
                    .Where(p => p.ProductId == item.ProductId && p.ProductTypeId == item.ProductTypeId)
                    .Include(t => t.ProductType).FirstOrDefaultAsync();
            
                if (productVariant == null)
                {
                    continue;
                }

                var cartProduct = new CartDto
                {
                    ProductId = product.Id,
                    ProductTitle = product.Title,
                    ProductTypeId = productVariant.ProductTypeId,
                    ProductType = productVariant.ProductType.Name,
                    Image = product.Image,
                    Price = productVariant.Price,
                    Quantity = item.Quantity
                };
            
                result.Data.Add(cartProduct);
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartDto>>> StoreCartItems(List<CartItem> cartItems)
        {
            var userId =  _authenticationService.GetUserId();
            cartItems.ForEach(item => item.UserId = userId);
            _dataContext.CartItems.AddRange(cartItems);
            await _dataContext.SaveChangesAsync();

            return await GetDbCartProducts();
        }

        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {
            var count = (await _dataContext.CartItems.Where(ci => ci.UserId == _authenticationService.GetUserId()).ToListAsync()).Count;
            return new ServiceResponse<int> { Data = count };

        }

        public async Task<ServiceResponse<List<CartDto>>> GetDbCartProducts(int? userId = null)
        {
            userId ??= _authenticationService.GetUserId();
            var cart = await _dataContext.CartItems.Where(cart => cart.UserId == userId).ToListAsync();
            return await GetCartProducts(cart);
        }

        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
        {
            cartItem.UserId = _authenticationService.GetUserId();
            var sameItemInCart = await _dataContext.CartItems
                .FirstOrDefaultAsync(cart =>
                    cart.ProductId == cartItem.ProductId &&
                    cart.ProductTypeId == cartItem.ProductTypeId && 
                    cart.UserId == cartItem.UserId);

            if (sameItemInCart == null)
            {
                _dataContext.CartItems.Add(cartItem);
            }
            else
            {
                sameItemInCart.Quantity += cartItem.Quantity;
            }

            await _dataContext.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
        {
            var item = await _dataContext.CartItems
                .FirstOrDefaultAsync(cart =>
                    cart.ProductId == productId &&
                    cart.ProductTypeId == productTypeId && 
                    cart.UserId == _authenticationService.GetUserId());
            
            if (item == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };
            }

            _dataContext.CartItems.Remove(item);
            await _dataContext.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateQauntity(CartItem cartItem)
        {
            var sameItemInCart = await _dataContext.CartItems
                .FirstOrDefaultAsync(cart =>
                    cart.ProductId == cartItem.ProductId &&
                    cart.ProductTypeId == cartItem.ProductTypeId && 
                    cart.UserId == _authenticationService.GetUserId());

            if (sameItemInCart == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };
            }

            sameItemInCart.Quantity = cartItem.Quantity;
            await _dataContext.SaveChangesAsync();
            return new ServiceResponse<bool>
            {
                Data = true
            };        
        }



}
    