using System.Net.Http.Json;
using Eshop.Shared.Models;

namespace Eshop.Client.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _http;
    public List<Category> Categories { get; set; } = new();

    public CategoryService(HttpClient http)
    {
        _http = http;
    }
        public async Task GetCategories()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Categories");
            if (response is { Data: not null })
            {
                Categories = response.Data;
            }
        }
}