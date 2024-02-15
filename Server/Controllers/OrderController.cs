using Ecommerce.Shared.Orders;
using Ecommerce.Shared;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Server.Services.Ordering;

namespace Ecommerce.Server.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderingService;

		public OrderController(IOrderService orderingService)
		{
			_orderingService = orderingService;
		}


		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<OrderDTO>>>> GetOrders()
		{
			var response = await _orderingService.GetOrders();
			return Ok(response);
		}

		[HttpGet]
		[Route("{orderId}")]
		public async Task<ActionResult<ServiceResponse<OrderDetailDTO>>> GetOrderDetails(int orderId)
		{
			var response = await _orderingService.GetOrderDetails(orderId);
			return Ok(response);
		}

		[HttpGet]
		[Route("/admin/all")]
		public async Task<ActionResult<ServiceResponse<List<OrderDTO>>>> GetAllOrders()
		{
			var response = await _orderingService.GetAllOrders();
			return Ok(response);
		}


		[HttpPut]
		[Route("/admin/update/{id}")]
		public async Task<ActionResult<ServiceResponse<List<OrderDTO>>>> UpdateOrder(int id, string status)
		{
			var response = await _orderingService.UpdateOrder(id, status);
			return Ok(response);
		}
	}
}
