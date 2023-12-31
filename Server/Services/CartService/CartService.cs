using Eshop.Server.Database;
using Eshop.Shared.DTOs;
using Eshop.Shared.Models.Cart;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Services.CartService;

public class CartService : ICartService
{
    private readonly DataContext _dataContext;

    public CartService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<ServiceResponse<List<CartDto>>> GetProductsInCart(List<CartItem> cartItems)
    {
        var result = new ServiceResponse<List<CartDto>>
        {
            Data = new List<CartDto>()
        };

        foreach (var item in cartItems)
        {
            var product = await _dataContext.Products.Where(p => p.Id == item.ProductId).FirstOrDefaultAsync();

            if (product == null)
            {
                continue;
            }

            var productVariant = await _dataContext.ProductVariants
                .Where(p => p.ProductId == item.ProductId && p.ProductTypeId == item.ProductTypeId)
                .Include(t => t.ProductType).FirstOrDefaultAsync();
            
            if (productVariant == null)
            {
                continue;
            }

            var cartProduct = new CartDto
            {
                ProductId = product.Id,
                ProductTitle = product.Title,
                ProductTypeId = productVariant.ProductTypeId,
                ProductType = productVariant.ProductType.Name,
                Image = product.Image,
                Price = productVariant.Price,
                Quantity = item.Quantity
            };
            
            result.Data.Add(cartProduct);
        }

        return result;
    }
}