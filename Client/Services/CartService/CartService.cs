using System.Net.Http.Json;
using Blazored.LocalStorage;
using Eshop.Shared.DTOs;
using Eshop.Shared.Models.Cart;

namespace Eshop.Client.Services.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;
    public event Action? OnCartChange;

    public CartService(ILocalStorageService localStorage, HttpClient httpClient)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
    }
    
    public async Task AddToCart(CartItem item)
    {
        var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        if (cart is null)
        {
            cart = new();
        }

        var itemAlreadyInCart = cart.Find(i => i.ProductId == item.ProductId && i.ProductTypeId == item.ProductTypeId);
        if (itemAlreadyInCart == null)
        {
            cart.Add(item);
        }
        else
        {
            itemAlreadyInCart.Quantity += item.Quantity;
        }
        
        await _localStorage.SetItemAsync("cart", cart);
        OnCartChange?.Invoke();
    }

    public async Task<List<CartItem>> GetCartItems()
    {
        var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart") ?? new List<CartItem>();
        return cart;
    }

    public async Task<List<CartDto>> GetCartProducts()
    {
        var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        var response = await _httpClient.PostAsJsonAsync("api/cart/products", cartItems);
        var cartProducts =
            await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartDto>>>();
        return cartProducts.Data;
    }

    public async Task RemoveItemFromCart(int productId, int productTypeId)
    {
        var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");

        var cartItemToRemove = cartItems?.Find(i => i.ProductId == productId && i.ProductTypeId == productTypeId);
        if (cartItemToRemove != null)
        {
            cartItems?.Remove(cartItemToRemove);
            await _localStorage.SetItemAsync("cart", cartItems);
            OnCartChange?.Invoke();
        }
    }

    public async Task UpdateQuantityForProduct(CartDto item)
    {
        var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");

        var cartItem = cartItems?.Find(i => i.ProductId == item.ProductId && i.ProductTypeId == item.ProductTypeId);
        if (cartItem != null)
        {
            cartItem.Quantity = item.Quantity;
            await _localStorage.SetItemAsync("cart", cartItems);
        }
    }
}