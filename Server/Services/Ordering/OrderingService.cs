using System.Security.Claims;
using Eshop.Server.Database;
using Eshop.Server.Services.Authentication;
using Eshop.Server.Services.CartService;
using Eshop.Shared.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Services.Ordering;

public class OrderingService : IOrderingService
{
    private readonly DataContext _dataContext;
    private readonly ICartService _cartService;
    private readonly IAuthenticationService _authenticationService;

    public OrderingService(DataContext dataContext, ICartService cartService ,IAuthenticationService authenticationService)
    {
        _dataContext = dataContext;
        _cartService = cartService;
        _authenticationService = authenticationService;
    }
    
    public async Task<ServiceResponse<bool>> PlaceOrder()
    {
        var products = (await _cartService.GetDbCartProducts()).Data;
        decimal totalPrice = 0;
        products?.ForEach(p => totalPrice += (p.Price * p.Quantity));

        var orderItems = new List<OrderItem>();
        products?.ForEach(p => orderItems.Add(new OrderItem
        {
            ProductId = p.ProductId,
            ProductTypeId = p.ProductTypeId,
            Quantity = p.Quantity,
            TotalPrice = p.Price * p.Quantity
        }));

        var order = new Order
        {
            UserId = _authenticationService.GetUserId(),
            OrderDate = DateTime.Now,
            TotalPrice = totalPrice,
            OrderItems = orderItems
        };

        _dataContext.Orders.Add(order);
        
        //Remove cart
        _dataContext.CartItems.RemoveRange(_dataContext.CartItems.Where(cart => cart.UserId == _authenticationService.GetUserId()));
        await _dataContext.SaveChangesAsync();

        return new ServiceResponse<bool> { Data = true };

    }

    public async Task<ServiceResponse<List<OrderDto>>> GetOrders()
    {
        var userId = _authenticationService.GetUserId();
        if (userId == null)
        {
            userId = _authenticationService.GetUserId();
        }

        var response = new ServiceResponse<List<OrderDto>>();
        var orders = await _dataContext.Orders
            .Include(o => o.OrderItems).
            ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == userId).
            OrderByDescending(o => o.OrderDate).
            ToListAsync();

        var ordersResponse = new List<OrderDto>();
        orders.ForEach(o => ordersResponse.Add(new OrderDto
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            TotalPrice = o.TotalPrice,
            Product = o.OrderItems.Count > 1 ?
                $"{o.OrderItems.First().Product.Title} and" +
                $" {o.OrderItems.Count - 1} more..." :
                o.OrderItems.First().Product.Title,
            Image = o.OrderItems.First().Product.Image
        }));

        response.Data = ordersResponse;
        return response;
    }

    public async Task<ServiceResponse<OrderDetailDto>> GetOrderDetails(int orderId)
    {
        var userId = _authenticationService.GetUserId();
        var response = new ServiceResponse<OrderDetailDto>();
        
        var order = await _dataContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.ProductType)
            .Where(o => o.UserId == userId && o.Id == orderId)
            .OrderByDescending(o => o.OrderDate)
            .FirstOrDefaultAsync();

        if (order == null)
        {
            response.Success = false;
            response.Message = "Order not found.";
            return response;
        }

        var orderDetailsResponse = new OrderDetailDto
        {
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            Products = new List<OrderProductDetailDto>()
        };

        order.OrderItems.ForEach(item =>
            orderDetailsResponse.Products.Add(new OrderProductDetailDto { 
                Id = item.ProductId,
                Image = item.Product.Image,
                ProductType = item.ProductType.Name,
                Quantity = item.Quantity,
                Title = item.Product.Title,
                TotalPrice = item.TotalPrice 
            }));

        response.Data = orderDetailsResponse;
        return response;
    }
    
}