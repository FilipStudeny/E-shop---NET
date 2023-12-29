using System.Net.Http.Json;
using Eshop.Shared.Models;

namespace Eshop.Client.Services.ProductService;

public class ProductService : IProductService
{
    public event Action? ChangeProducts;
    public string Message { get; set; }
    private readonly HttpClient _http;
    
    public List<Product> Products { get; set; } = new ();

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task GetProducts(string? category = null)
    {
        var response = category == null
            ? await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product")
            : await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{category}");

        if (response is { Data: not null })
        {
            Products = response.Data;
        }
        
        ChangeProducts?.Invoke();
    }

    public async Task<ServiceResponse<Product>> GetProduct(int id)
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{id}");
        return response;
    }

    public async Task SearchProducts(string search)
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/search/{search}");

        if (response is { Data: not null })
        {
            Products = response.Data;
        }

        if (Products.Count == 0) Message = "No products found.";
        ChangeProducts?.Invoke();
    }

    public async Task<List<string>> GetProductSearchSuggestions(string search)
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/suggestions/{search}");
        return response?.Data;
        
    }


}