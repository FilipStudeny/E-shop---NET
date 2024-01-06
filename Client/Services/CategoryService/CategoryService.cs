using System.Net.Http.Json;
using Eshop.Shared.Models;
using Eshop.Shared.Models.ProductModels;

namespace Eshop.Client.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _http;
    public event Action? OnChange;
    public List<Category> Categories { get; set; } = new();
    public List<Category> AdminCategories { get; set; } = new();

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

    public async Task GetAdminCategories()
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Categories/admin");
        if (response is { Data: not null })
        {
            AdminCategories = response.Data;
        }
    }

    public async Task AddCategory(Category category)
    {
        var response = await _http.PostAsJsonAsync("api/Categories/admin", category);
        AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
        await GetCategories();
        OnChange?.Invoke();
    }

    public async Task UpdateCategory(Category category)
    {
        var response = await _http.PutAsJsonAsync("api/Categories/admin", category);
        AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>())?.Data!;
        await GetCategories();
        OnChange?.Invoke();
    }

    public async Task DeleteCategory(int id)
    {
        var response = await _http.DeleteAsync($"api/Categories/admin/{id}");
        AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>())?.Data!;
        await GetCategories();
        OnChange?.Invoke();
    }

    public Category CreateNewCategory()
    {
        var newCategory = new Category
        {
            IsNew = true,
            Editing = true
        };
        AdminCategories.Add(newCategory);
        OnChange?.Invoke();
        return newCategory;
    }
}

