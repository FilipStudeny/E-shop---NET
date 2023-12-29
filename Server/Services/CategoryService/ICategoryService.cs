using Eshop.Shared.Models;

namespace Eshop.Server.Services.CategoryService;

public interface ICategoryService
{
    Task<ServiceResponse<List<Category>>> GetCategories();
}