using Ecommerce.Shared.Orders;
using Ecommerce.Shared;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Server.Services.Ordering;

namespace Ecommerce.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICartService _cartService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CartController(ICartService cartService, IHttpContextAccessor httpContextAccessor)
		{
			_cartService = cartService;
			_httpContextAccessor = httpContextAccessor;
		}

		[HttpPost("products")]
		public async Task<ActionResult<ServiceResponse<List<CartDTO>>>> GetCartProducts(List<CartItem> cartItems)
		{
			var result = await _cartService.GetCartProducts(cartItems);
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<List<CartDTO>>>> StoreCartItems(List<CartItem> cartItems)
		{
			var result = await _cartService.StoreCartItems(cartItems);
			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<ActionResult<ServiceResponse<bool>>> AddToCart(CartItem cartItem)
		{
			var result = await _cartService.AddToCart(cartItem);
			return Ok(result);
		}

		[HttpPut("update-quantity")]
		public async Task<ActionResult<ServiceResponse<bool>>> UpdateProductQuantity(CartItem cartItem)
		{
			var result = await _cartService.UpdateQauntity(cartItem);
			return Ok(result);
		}

		[HttpDelete("{productId}/{productTypeId}")]
		public async Task<ActionResult<ServiceResponse<bool>>> RemoveItemFromCart(int productId, int productTypeId)
		{
			var result = await _cartService.RemoveItemFromCart(productId, productTypeId);
			return Ok(result);
		}

		[HttpGet("count")]
		public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCount()
		{
			return await _cartService.GetCartItemsCount();
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<CartDTO>>>> GetDbCartProducts()
		{
			var result = await _cartService.GetDbCartProducts();
			return Ok(result);
		}

	}
}
