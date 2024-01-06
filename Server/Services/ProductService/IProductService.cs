using Eshop.Shared.DTOs;
using Eshop.Shared.Models;
using Eshop.Shared.Models.ProductModels;

namespace Eshop.Server.Services.ProductService;

public interface IProductService
{ 
    Task<ServiceResponse<List<Product>>> GetProducts();
    Task<ServiceResponse<Product>> GetProduct(int id);
    
    Task<ServiceResponse<List<Product>>> GetProductsByCategory(string category);
    Task<ServiceResponse<ProductSearchDto>> SearchProducts(string search, int page);
    Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string search);
    Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
    Task<ServiceResponse<List<Product>>> GetAdminProducts();




}