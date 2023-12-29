using Eshop.Server.Database;
using Eshop.Shared.Models;
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
        var categories = await _dataContext.Categories.ToListAsync();
        return new ServiceResponse<List<Category>>
        {
            Data = categories
        };
    }

}