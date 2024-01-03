using System.Net.Http.Json;
using System.Security.Principal;
using Blazored.LocalStorage;
using Eshop.Shared.DTOs;
using Eshop.Shared.Models.Cart;
using Microsoft.AspNetCore.Components.Authorization;

namespace Eshop.Client.Services.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    public event Action? OnCartChange;

    public CartService(ILocalStorageService localStorage, HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
    }

    private async Task<bool> IsAuthenticated()
    {
        return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
    }
    
    public async Task AddToCart(CartItem item)
    {
        if (await IsAuthenticated())
        {
            await _httpClient.PostAsJsonAsync("api/cart/add", item);
        }
        else
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart") ?? new List<CartItem>();
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
        }
        await GetCartItemCount();

    }

    
    public async Task<List<CartDto>> GetCartProducts()
    {

        if (await IsAuthenticated())
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<CartDto>>>("api/cart");
            return response.Data;
        }
        else
        {
            var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cartItems == null)
            {
                return new List<CartDto>();
            }
            var response = await _httpClient.PostAsJsonAsync("api/cart/products", cartItems);
            var cartProducts =
                await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartDto>>>();
            return cartProducts.Data;
        }
    }

    public async Task RemoveItemFromCart(int productId, int productTypeId)
    {
        if (await IsAuthenticated())
        {
            await _httpClient.DeleteAsync($"api/Cart/{productId}/{productTypeId}");
        }
        else
        {
            var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            var cartItemToRemove = cartItems?.Find(i => i.ProductId == productId && i.ProductTypeId == productTypeId);
            if (cartItemToRemove != null)
            {
                cartItems?.Remove(cartItemToRemove);
                await _localStorage.SetItemAsync("cart", cartItems);
            }
        }
    }

    public async Task UpdateQuantityForProduct(CartDto item)
    {
        if (await IsAuthenticated())
        {
            await _httpClient.PutAsJsonAsync("api/cart/update-quantity", new CartItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                ProductTypeId = item.ProductTypeId
            });
        }
        else
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

    public async Task StoreCartItemsInDatabase(bool emptyLocalCart = false)
    {
        var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        if (cart == null)
        {
            return;
        }

        await _httpClient.PostAsJsonAsync("api/cart", cart);

        if (emptyLocalCart)
        {
            await _localStorage.RemoveItemAsync("cart");
        }

    }

    public async Task GetCartItemCount()
    {
        if (await IsAuthenticated())
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");
            var count = response.Data;
            await _localStorage.SetItemAsync<int>("itemCount", count);
            
        }
        else
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            await _localStorage.SetItemAsync<int>("itemCount", cart != null ? cart.Count : 0);
        }
        
        OnCartChange.Invoke();
    }
}