using Blazored.LocalStorage;
using Ecommerce.Shared;
using Ecommerce.Shared.Orders;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.Ordering
{
	public class CartService : ICartService
	{
		private readonly ILocalStorageService localStorage;
		private readonly HttpClient httpClient;
		private readonly AuthenticationStateProvider authenticationStateProvider;

		public event Action? OnCartChange;

		public CartService(ILocalStorageService localStorage, HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
		{
			this.localStorage = localStorage;
			this.httpClient = httpClient;
			this.authenticationStateProvider = authenticationStateProvider;
		}

		private async Task<bool> IsAuthenticated()
		{
			return (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
		}

		public async Task AddToCart(CartItem item)
		{
			if (await IsAuthenticated())
			{
				await httpClient.PostAsJsonAsync("api/cart/add", item);
			}
			else
			{
				var cart = await localStorage.GetItemAsync<List<CartItem>>("cart") ?? new List<CartItem>();
				var itemAlreadyInCart = cart.Find(i => i.BookId == item.BookId && i.BookTypeId == item.BookTypeId);
				if (itemAlreadyInCart == null)
				{
					cart.Add(item);
				}
				else
				{
					itemAlreadyInCart.Quantity += item.Quantity;
				}
				await localStorage.SetItemAsync("cart", cart);
			}
			await GetCartItemCount();

		}


		public async Task<List<CartDTO>> GetCartProducts()
		{

			if (await IsAuthenticated())
			{
				var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<CartDTO>>>("api/cart");
				return response.Data;
			}
			else
			{
				var cartItems = await localStorage.GetItemAsync<List<CartItem>>("cart");
				if (cartItems == null)
				{
					return new List<CartDTO>();
				}
				var response = await httpClient.PostAsJsonAsync("api/cart/products", cartItems);
				var cartProducts =
					await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartDTO>>>();
				return cartProducts.Data;
			}
		}

		public async Task RemoveItemFromCart(int productId, int productTypeId)
		{
			if (await IsAuthenticated())
			{
				await httpClient.DeleteAsync($"api/Cart/{productId}/{productTypeId}");
			}
			else
			{
				var cartItems = await localStorage.GetItemAsync<List<CartItem>>("cart");

				var cartItemToRemove = cartItems?.Find(i => i.BookId == productId && i.BookTypeId == productTypeId);
				if (cartItemToRemove != null)
				{
					cartItems?.Remove(cartItemToRemove);
					await localStorage.SetItemAsync("cart", cartItems);
				}
			}

            await GetCartItemCount();

        }

        public async Task UpdateQuantityForProduct(CartDTO item)
		{
			if (await IsAuthenticated())
			{
				await httpClient.PutAsJsonAsync("api/cart/update-quantity", new CartItem
				{
					BookId = item.BookId,
					Quantity = item.Quantity,
					BookTypeId = item.BookTypeId
				});
			}
			else
			{
				var cartItems = await localStorage.GetItemAsync<List<CartItem>>("cart");
				var cartItem = cartItems?.Find(i => i.BookId == item.BookId && i.BookTypeId == item.BookTypeId);
				if (cartItem != null)
				{
					cartItem.Quantity = item.Quantity;
					await localStorage.SetItemAsync("cart", cartItems);
				}
			}

		}

		public async Task StoreCartItemsInDatabase(bool emptyLocalCart = false)
		{
			var cart = await localStorage.GetItemAsync<List<CartItem>>("cart");
			if (cart == null)
			{
				return;
			}

			await httpClient.PostAsJsonAsync("api/cart", cart);

			if (emptyLocalCart)
			{
				await localStorage.RemoveItemAsync("cart");
			}

		}

		public async Task GetCartItemCount()
		{
			if (await IsAuthenticated())
			{
				var response = await httpClient.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");
				var count = response.Data;
				await localStorage.SetItemAsync<int>("itemCount", count);

			}
			else
			{
				var cart = await localStorage.GetItemAsync<List<CartItem>>("cart");
				await localStorage.SetItemAsync<int>("itemCount", cart != null ? cart.Count : 0);
			}

			OnCartChange.Invoke();
		}


	}
}
