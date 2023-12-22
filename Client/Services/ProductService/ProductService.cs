using System.Net.Http.Json;

namespace Eshop.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _http;
    public List<Product> Products { get; set; } = new ();

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task GetProducts()
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product");
        if (response is { Data: not null })
        {
            Products = response.Data;
        }
    }

    public async Task<ServiceResponse<Product>> GetProduct(int id)
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{id}");
        return response;
        
    }
}