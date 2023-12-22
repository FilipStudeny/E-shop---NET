using Eshop.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _dataContext;

    public ProductService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<ServiceResponse<List<Product>>> GetProducts()
    {
        var response = new ServiceResponse<List<Product>>
        {
            Data = await _dataContext.Products.ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<Product>> GetProduct(int id)
    {
        var response = new ServiceResponse<Product>();
        var product = await _dataContext.Products.FindAsync(id);
        if (product is null)
        {
            response.Success = false;
            response.Message = "Product doesn't exist.";
        }
        else
        {
            response.Data = product;
        }

        return response;
    }
}