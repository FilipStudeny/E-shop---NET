using Eshop.Server.Services.Authentication;
using Eshop.Server.Services.Ordering;
using Eshop.Shared.Models.Order;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderingService _orderingService;
    private readonly IAuthenticationService _authenticationService;

    public OrderController(IOrderingService orderingService, IAuthenticationService authenticationService)
    {
        _orderingService = orderingService;
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<bool>>> PlaceOrder()
    {
        var response = await _orderingService.PlaceOrder();
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<OrderDto>>>> GetOrders()
    {
        var response = await _orderingService.GetOrders();
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{orderId}")]
    public async Task<ActionResult<ServiceResponse<OrderDetailDto>>> GetOrderDetails(int orderId)
    {
        var response = await _orderingService.GetOrderDetails(orderId);
        return Ok(response);
    }

}