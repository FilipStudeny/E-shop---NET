using Eshop.Server.Database;
using Eshop.Shared.DTOs;
using Eshop.Shared.Models;
using Eshop.Shared.Models.ProductModels;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
    {
        _dataContext = dataContext;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ServiceResponse<List<Product>>> GetProducts()
    {
        var response = new ServiceResponse<List<Product>>
        {
            Data = await _dataContext.Products
                .Where(p => p.Visible && !p.Deleted)
                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .Include(p => p.Images)
                .ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<Product>> GetProduct(int id)
    {
        var response = new ServiceResponse<Product>();
        Product? product = null;

        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.IsInRole("Admin"))
        {
             product  = await _dataContext.Products
                .Include(p => p.Variants.Where(v => !v.Deleted))
                .ThenInclude(variant => variant.ProductType)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);
        }
        else
        {
            product  = await _dataContext.Products
                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .ThenInclude(variant => variant.ProductType)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id && p.Visible && !p.Deleted);
        }
        
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
                .Where( p => p.Category.Url.ToLower().Equals(category.ToLower()) && p.Visible && !p.Deleted)
                .Include( p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .Include(p => p.Images)
                .ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<ProductSearchDto>> SearchProducts(string search, int page)
    {
        var numberOfResults = 2f;
        var pageCount = Math.Ceiling((await FindBySearchText(search)).Count / numberOfResults);

        var products = await _dataContext.Products
            .Where(p => p.Title.ToLower().Contains(search.ToLower()) ||
                        p.Description.ToLower().Contains(search.ToLower()) && 
                                                         p.Visible && !p.Deleted)
            .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
            .Include(p => p.Images)
            .Skip((page - 1) * (int)numberOfResults)
            .Take((int)numberOfResults)
            .ToListAsync();
        
        var response = new ServiceResponse<ProductSearchDto>
        {
            Data = new ProductSearchDto
            {
                Products = products,
                Pages = (int)pageCount,
                CurrentPage = page
            }
        };

        return response;
    }

    public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string search)
    {
        var products = await FindBySearchText(search);
        
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

    private async Task<List<Product>> FindBySearchText(string search)
    {
        return await _dataContext.Products
            .Where(p => p.Title.ToLower().Contains(search.ToLower()) ||
                        p.Description.ToLower().Contains(search.ToLower()) && 
                                                         p.Visible && !p.Deleted)
            .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
            .ToListAsync();
    }

    public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
    {
        var products = new ServiceResponse<List<Product>>
        {
            Data = await _dataContext.Products
                .Where(p => p.FeaturedProduct && p.Visible && !p.Deleted)
                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .Include(p => p.Images)
                .ToListAsync()
        };

        return products;
    }

    public async Task<ServiceResponse<List<Product>>> GetAdminProducts()
    {
        var response = new ServiceResponse<List<Product>>
        {
            Data = await _dataContext.Products
                .Where(p => !p.Deleted)
                .Include(p => p.Variants.Where(v => !v.Deleted))
                .ThenInclude(pt => pt.ProductType)
                .Include(p => p.Images)
                .ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<Product>> CreateProduct(Product product)
    {
        foreach (var variant in product.Variants)
        {
            variant.ProductType = null;
        }

        _dataContext.Products.Add(product);
        await _dataContext.SaveChangesAsync();
        return new ServiceResponse<Product> { Data = product };
    }

    public async Task<ServiceResponse<Product>> UpdateProduct(Product product)
    {
        var dbProduct = await _dataContext.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == product.Id);
        
        if (dbProduct == null)
        {
            return new ServiceResponse<Product>
            {
                Success = false,
                Message = "Product not found."
            };
        }

        dbProduct.Title = product.Title;
        dbProduct.Description = product.Description;
        dbProduct.Image = product.Image;
        dbProduct.CategoryId = product.CategoryId;
        dbProduct.Visible = product.Visible;
        dbProduct.FeaturedProduct = product.FeaturedProduct;

        var productImages = dbProduct.Images;
        _dataContext.Images.RemoveRange(productImages);
        dbProduct.Images = product.Images;
        
        foreach (var variant in product.Variants)
        {
            var dbVariant = await _dataContext.ProductVariants
                .SingleOrDefaultAsync(v => v.ProductId == variant.ProductId && v.ProductTypeId == variant.ProductTypeId);
            if (dbVariant == null)
            {
                variant.ProductType = null;
                _dataContext.ProductVariants.Add(variant);
            }
            else
            {
                dbVariant.ProductTypeId = variant.ProductTypeId;
                dbVariant.Price = variant.Price;
                dbVariant.OriginalPrice = variant.OriginalPrice;
                dbVariant.Visible = variant.Visible;
                dbVariant.Deleted = variant.Deleted;
            }
        }

        await _dataContext.SaveChangesAsync();
        return new ServiceResponse<Product> { Data = product };
    }

    public async Task<ServiceResponse<bool>> DeleteProduct(int id)
    {
        var dbProduct = await _dataContext.Products.FindAsync(id);
        if (dbProduct == null)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Success = false,
                Message = "Product not found."
            };
        }

        dbProduct.Deleted = true;
        await _dataContext.SaveChangesAsync();
        return new ServiceResponse<bool> { Data = true };
    }
}






















