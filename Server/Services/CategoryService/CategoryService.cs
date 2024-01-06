using Eshop.Server.Database;
using Eshop.Shared.Models;
using Eshop.Shared.Models.ProductModels;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly DataContext _dataContext;

    public CategoryService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<ServiceResponse<List<Category>>> GetCategories()
    {
        var categories = await _dataContext.Categories.Where(c => !c.Deleted && c.Visible).ToListAsync();
        return new ServiceResponse<List<Category>>
        {
            Data = categories
        };
    }

    public async Task<ServiceResponse<List<Category>>> GetAdminCategories()
    {
        var categories = await _dataContext.Categories.Where(c => !c.Deleted).ToListAsync();
        return new ServiceResponse<List<Category>>
        {
            Data = categories
        };    
    }

    public async Task<ServiceResponse<List<Category>>> AddCategory(Category category)
    {
        category.Editing = category.IsNew = false;;
        _dataContext.Categories.Add(category);
        await _dataContext.SaveChangesAsync();
        return await GetAdminCategories();
    }

    public async Task<ServiceResponse<List<Category>>> UpdateCategory(Category category)
    {
        var categoryInDb = await GetCategoryById(category.Id);
        if (categoryInDb == null)
        {
            return new ServiceResponse<List<Category>>
            {
                Success = false,
                Message = "Category not found"
            };   
        }

        categoryInDb.Name = category.Name;
        categoryInDb.Url = category.Url;
        categoryInDb.Visible = category.Visible;

        await _dataContext.SaveChangesAsync();
        return await GetAdminCategories();
    }

    public async Task<ServiceResponse<List<Category>>> DeleteCategory(int categoryId)
    {
        var category = await GetCategoryById(categoryId);
        if (category == null)
        {
            return new ServiceResponse<List<Category>>
            {
                Success = false,
                Message = "Category not found"
            };
        }

        category.Deleted = true;
        await _dataContext.SaveChangesAsync();
        return await GetAdminCategories();
    }

    private async Task<Category?> GetCategoryById(int categoryId)
    {
        return await _dataContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
    }
}