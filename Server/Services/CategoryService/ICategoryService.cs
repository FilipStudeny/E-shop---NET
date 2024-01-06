using Eshop.Shared.Models;
using Eshop.Shared.Models.ProductModels;

namespace Eshop.Server.Services.CategoryService;

public interface ICategoryService
{
    Task<ServiceResponse<List<Category>>> GetCategories();
    Task<ServiceResponse<List<Category>>> GetAdminCategories();
    Task<ServiceResponse<List<Category>>> AddCategory(Category category);
    Task<ServiceResponse<List<Category>>> UpdateCategory(Category category);
    Task<ServiceResponse<List<Category>>> DeleteCategory(int categoryId);


}