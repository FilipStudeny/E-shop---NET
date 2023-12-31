﻿using System.Net.Http.Json;
using Eshop.Shared.DTOs;
using Eshop.Shared.Models;
using Eshop.Shared.Models.ProductModels;

namespace Eshop.Client.Services.ProductService;

public class ProductService : IProductService
{
    public string Message { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public string LastSearch { get; set; }
    
    private readonly HttpClient _http;
    public event Action? ChangeProducts;

    public List<Product> Products { get; set; } = new ();
    public List<Product> AdminProducts { get; set; } = new();

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task GetProducts(string? category = null)
    {
        var response = category == null
            ? await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/featured")
            : await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{category}");

        if (response is { Data: not null })
        {
            Products = response.Data;
        }

        CurrentPage = 1;
        PageCount = 0;

        if (Products.Count == 0)
        {
            Message = "No products found.";
        }
        ChangeProducts?.Invoke();
    }

    public async Task<ServiceResponse<Product>> GetProduct(int id)
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{id}");
        return response;
    }

    public async Task SearchProducts(string search, int page)
    {
        LastSearch = search;
        var response = await _http.GetFromJsonAsync<ServiceResponse<ProductSearchDto>>($"api/product/search/{search}/{page}");

        if (response is { Data: not null })
        {
            Products = response.Data.Products;
            CurrentPage = response.Data.CurrentPage;
            PageCount = response.Data.Pages;
        }

        if (Products.Count == 0) Message = "No products found.";
        ChangeProducts?.Invoke();
    }

    public async Task<List<string>> GetProductSearchSuggestions(string search)
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/suggestions/{search}");
        return response?.Data;
        
    }

    public async Task GetAdminProducts()
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/admin");
        AdminProducts = response?.Data;
        CurrentPage = 1;
        PageCount = 0;

        if (AdminProducts is { Count: 0 })
        {
            Message = "No products found";
        }
    }

    public async Task<Product> CreateProduct(Product product)
    {
        var response = await _http.PostAsJsonAsync($"api/product/admin", product);
        var newProduct = (await response.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
        return newProduct;
    }


    public async Task<Product> UpdateProduct(Product product)
    {
        var response = await _http.PutAsJsonAsync($"api/product/admin", product);
        return (await response.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
    }

    public async Task DeleteProduct(Product product)
    {
        await _http.DeleteAsync($"api/product/admin/{product.Id}");
    }
    
    
}