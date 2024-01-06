using System.Net.Http.Json;
using Eshop.Shared.Models.ProductModels;

namespace Eshop.Client.Services.ProductTypeService;

public class ProductTypeService : IProductTypeService
{
    private readonly HttpClient _httpClient;
    public event Action? OnChange;
    public List<ProductType> ProductTypes { get; set; } = new();

    public ProductTypeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
    public async Task GetProductTypes()
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<ProductType>>>("api/producttype");
        ProductTypes = response?.Data;
    }

    public async Task AddProductType(ProductType productType)
    {
        var response = await _httpClient.PostAsJsonAsync("api/producttype", productType);
        ProductTypes = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<ProductType>>>()).Data;
        OnChange?.Invoke();
    }

    public async Task UpdateProductType(ProductType productType)
    {
        var response = await _httpClient.PutAsJsonAsync("api/producttype", productType);
        ProductTypes = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<ProductType>>>()).Data;
        OnChange?.Invoke();    }

    public ProductType CreateNewProductType()
    {
        var newType = new ProductType { IsNew = true, Editing = true };
        ProductTypes.Add(newType);
        OnChange?.Invoke();
        return newType;
    }
}