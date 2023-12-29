﻿using Eshop.Server.Database;
using Eshop.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _dataContext;

    public ProductService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<ServiceResponse<List<Product>>> GetProducts()
    {
        var response = new ServiceResponse<List<Product>>
        {
            Data = await _dataContext.Products.Include(p => p.Variants).ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<Product>> GetProduct(int id)
    {
        var response = new ServiceResponse<Product>();
        var product  = await _dataContext.Products
            .Include(p => p.Variants)
            .ThenInclude(variant => variant.ProductType)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (product is null)
        {
            response.Success = false;
            response.Message = "Product doesn't exist.";
        }
        else
        {
            response.Data = product;
        }

        return response;
    }

    public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string category)
    {
        var response = new ServiceResponse<List<Product>>
        {
            Data = await _dataContext.Products
                .Where( p => p.Category.Url.ToLower().Equals(category.ToLower()))
                .Include( p => p.Variants)
                .ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<List<Product>>> SearchProducts(string search)
    {
        var products = new ServiceResponse<List<Product>>
        {
            Data = await _dataContext.Products
                .Where(p => p.Title.ToLower().Contains(search.ToLower()) ||
                            p.Description.ToLower().Contains(search.ToLower()))
                .Include(p => p.Variants)
                .ToListAsync()
        };

        return products;
    }

    public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string search)
    {
        var products = await _dataContext.Products
            .Where(p => p.Title.ToLower().Contains(search.ToLower()) ||
                        p.Description.ToLower().Contains(search.ToLower()))
            .Include(p => p.Variants)
            .ToListAsync();
        
        List<string> productTitles = new();
        foreach (var product in products)
        {
            if (product.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
            {
                productTitles.Add(product.Title);
            }

            if (product?.Description != null)
            {
                //search from description
                var punctuation = product.Description.Where(char.IsPunctuation).Distinct().ToArray(); //remove !.?,;. ....
                var words = product.Description.Split().Select(s => s.Trim(punctuation));

                foreach (var word in words)
                {
                    if (word.Contains(search, StringComparison.OrdinalIgnoreCase) && !productTitles.Contains(word))
                    {
                        productTitles.Add(word);
                    }
                }
            }
        }
        return new ServiceResponse<List<string>>
        {
            Data = productTitles,
        };

    }
}






















