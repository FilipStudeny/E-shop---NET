using Eshop.Shared.Models;
using Eshop.Shared.Models.ProductModels;

namespace Eshop.Client.Services.ProductService;

public interface IProductService
{
    event Action ChangeProducts;
    string Message { get; set; }
    int CurrentPage { get; set; }
    int PageCount { get; set; }
    string LastSearch { get; set; }
    
    
    List<Product> Products { get; set; }
    List<Product> AdminProducts { get; set; }

    Task GetProducts(string? category = null);
    Task<ServiceResponse<Product>> GetProduct(int id);
    Task SearchProducts(string search, int page);
    Task<List<string>> GetProductSearchSuggestions(string search);
    Task GetAdminProducts();

    Task<Product> CreateProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task DeleteProduct(Product product);


}