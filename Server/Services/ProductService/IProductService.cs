namespace Eshop.Server.Services.ProductService;

public interface IProductService
{ 
    Task<ServiceResponse<List<Product>>> GetProducts();
    Task<ServiceResponse<Product>> GetProduct(int id);

}