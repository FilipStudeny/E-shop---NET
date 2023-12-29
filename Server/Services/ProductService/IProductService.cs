using Eshop.Shared.Models;

namespace Eshop.Server.Services.ProductService;

public interface IProductService
{ 
    Task<ServiceResponse<List<Product>>> GetProducts();
    Task<ServiceResponse<Product>> GetProduct(int id);
    
    Task<ServiceResponse<List<Product>>> GetProductsByCategory(string category);
    Task<ServiceResponse<List<Product>>> SearchProducts(string search);
    Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string search);


}