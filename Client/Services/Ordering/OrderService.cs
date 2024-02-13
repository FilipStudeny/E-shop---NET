using Ecommerce.Client.Services.AuthenticationService;
using Ecommerce.Shared.Orders;
using Ecommerce.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.Ordering
{
	public class OrderService : IOrderService
	{
		private readonly HttpClient _httpClient;
		private readonly IAuthenticationService _authenticationService;
		private readonly NavigationManager _navigationManager;

		public OrderService(HttpClient httpClient, IAuthenticationService authenticationService, NavigationManager navigationManager)
		{
			_httpClient = httpClient;
			_authenticationService = authenticationService;
			_navigationManager = navigationManager;
		}

		public async Task<string> PlaceOrder()
		{
			if (!await _authenticationService.IsAuthenticated()) return "/login";
			var response = await _httpClient.PostAsync("api/payment/checkout", null);
			var url = await response.Content.ReadAsStringAsync();
			return url;

		}

		public async Task<List<OrderDTO>> GetOrders()
		{
			var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderDTO>>>("api/order");
			return response.Data;
		}

		public async Task<OrderDetailDTO> GetOrderDetails(int orderId)
		{
			var response = await _httpClient.GetFromJsonAsync<ServiceResponse<OrderDetailDTO>>($"api/order/{orderId}");
			return response.Data;
		}
	}
}