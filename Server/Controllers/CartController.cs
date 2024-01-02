using System.Security.Claims;
using Eshop.Server.Services.CartService;
using Eshop.Shared.DTOs;
using Eshop.Shared.Models.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost]
    [Route("products")]
    public async Task<ActionResult<ServiceResponse<List<CartDto>>>> GetCartProducts(List<CartItem> cartItems)
    {
        var result = await _cartService.GetProductsInCart(cartItems);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<CartDto>>>> StoreCartInDatabase(List<CartItem> cartItems)
    {
        var result = await _cartService.StoreCartItemsInDatabase(cartItems);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("count")]
    public async Task<ActionResult<ServiceResponse<int>>> GetCartItemCount()
    {
        return await _cartService.GetCartItemCount();
    }
    
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<CartDto>>>> GetCart()
    {
        return Ok(await _cartService.GetStoredCart());
    }
    
}