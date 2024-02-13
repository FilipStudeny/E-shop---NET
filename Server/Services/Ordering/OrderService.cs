using Ecommerce.Server.Database;
using Ecommerce.Server.Services.UserService;
using Ecommerce.Shared.Orders;
using Ecommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services.Ordering
{
	public class OrderService : IOrderService
	{
		private readonly DataContext dataContext;
		private readonly ICartService cartService;
		private readonly IUserService authenticationService;

		public OrderService(DataContext dataContext, ICartService cartService, IUserService authenticationService)
        {
			this.dataContext = dataContext;
			this.cartService = cartService;
			this.authenticationService = authenticationService;
		}

		public async Task<ServiceResponse<bool>> PlaceOrder(int userId)
		{
			var products = (await cartService.GetDbCartProducts(userId)).Data;
			decimal totalPrice = 0;
			products?.ForEach(p => totalPrice += (p.Price * p.Quantity));

			var orderItems = new List<OrderItem>();
			products?.ForEach(p => orderItems.Add(new OrderItem
			{
				BookId = p.BookId,
				BookTypeId = p.BookTypeId,
				Quantity = p.Quantity,
				TotalPrice = p.Price * p.Quantity
			}));

			var order = new Order
			{
				UserId = userId,
				OrderDate = DateTime.Now,
				TotalPrice = totalPrice,
				OrderItems = orderItems
			};

			dataContext.Orders.Add(order);

			//Remove cart
			dataContext.CartItems.RemoveRange(dataContext.CartItems.Where(cart => cart.UserId == userId));
			await dataContext.SaveChangesAsync();

			return new ServiceResponse<bool> { Data = true };
		}

		public async Task<ServiceResponse<List<OrderDTO>>> GetOrders()
		{
			var userId = authenticationService.GetUserId();
			if (userId == null)
			{
				userId = authenticationService.GetUserId();
			}

			var response = new ServiceResponse<List<OrderDTO>>();
			var orders = await dataContext.Orders
				.Include(o => o.OrderItems).
				ThenInclude(oi => oi.Book)
				.Where(o => o.UserId == userId).
				OrderByDescending(o => o.OrderDate).
				ToListAsync();

			var ordersResponse = new List<OrderDTO>();
			orders.ForEach(o => ordersResponse.Add(new OrderDTO
			{
				Id = o.Id,
				OrderDate = o.OrderDate,
				TotalPrice = o.TotalPrice,
				Product = o.OrderItems.Count > 1 ?
					$"{o.OrderItems.First().Book.Title} and" +
					$" {o.OrderItems.Count - 1} more..." :
					o.OrderItems.First().Book.Title,
				Image = o.OrderItems.First().Book.DefaultImageUrl
			}));

			response.Data = ordersResponse;
			return response;
		}

		public async Task<ServiceResponse<OrderDetailDTO>> GetOrderDetails(int orderId)
		{
			var userId = authenticationService.GetUserId();
			var response = new ServiceResponse<OrderDetailDTO>();

			var order = await dataContext.Orders
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.Book)
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.BookType)
				.Where(o => o.UserId == userId && o.Id == orderId)
				.OrderByDescending(o => o.OrderDate)
				.FirstOrDefaultAsync();

			if (order == null)
			{
				response.Success = false;
				response.Message = "Order not found.";
				return response;
			}

			var orderDetailsResponse = new OrderDetailDTO
			{
				OrderDate = order.OrderDate,
				TotalPrice = order.TotalPrice,
				Books = new List<OrderBookDetailDTO>()
			};

			order.OrderItems.ForEach(item =>
				orderDetailsResponse.Books.Add(new OrderBookDetailDTO
				{
					Id = item.BookId,
					Image = item.Book.DefaultImageUrl,
					BookType = item.BookType.Name,
					Quantity = item.Quantity,
					Title = item.Book.Title,
					TotalPrice = item.TotalPrice
				}));

			response.Data = orderDetailsResponse;
			return response;
		}
	}
}
