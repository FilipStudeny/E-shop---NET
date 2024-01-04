using System.Net.Http.Json;
using Eshop.Client.Services.AuthenticationService;
using Eshop.Shared.Models.Order;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Eshop.Client.Services.OrderService;

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

    public async Task<List<OrderDto>> GetOrders()
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderDto>>>("api/order");
        return response.Data;
    }

    public async Task<OrderDetailDto> GetOrderDetails(int orderId)
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<OrderDetailDto>>($"api/order/{orderId}");
        return response.Data;
    }
}