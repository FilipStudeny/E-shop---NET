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

    public async Task PlaceOrder()
    {
        if (await _authenticationService.IsAuthenticated())
        {
            await _httpClient.PostAsync("api/order", null);
        }
        else
        {
            _navigationManager.NavigateTo("/login");
        }
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