using Eshop.Shared.Models;

namespace Eshop.Client.Services.ProductService;

public interface IProductService
{
    List<Product> Products { get; set; }
    Task GetProducts(string? category = null);
    Task<ServiceResponse<Product>> GetProduct(int id);
    Task SearchProducts(string search);
    Task<List<string>> GetProductSearchSuggestions(string search);
    
    
    event Action ChangeProducts;
    string Message { get; set; }
}