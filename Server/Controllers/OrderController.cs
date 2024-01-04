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

    public OrderController(IOrderingService orderingService)
    {
        _orderingService = orderingService;
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