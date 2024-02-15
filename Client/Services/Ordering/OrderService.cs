using Ecommerce.Client.Services.AuthenticationService;
using Ecommerce.Shared.Orders;
using Ecommerce.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Ecommerce.Shared.DTOs.Books;
using System.Net.Http;

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

		public async Task<List<OrderDTO>> GetAllOrders()
		{
			var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderDTO>>>("api/order/admin/all");
			return response.Data;
		}

		public async Task<ServiceResponse<bool>> UpdateOrder(int id, string status)
		{
			var response = await _httpClient.PutAsJsonAsync($"api/order/admin/update/{id}", status);
			var responseData = (await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>());
			if (responseData == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Failed to update order, try again later" };
			}
			return responseData;
		}
	}
}