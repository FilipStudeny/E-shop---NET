using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;

namespace Ecommerce.Server.Services.CategoryService
{
	public interface ICategoryService
	{
		Task<ServiceResponse<List<Category>>> GetCategories();
		Task<ServiceResponse<Category>> AddCategory(CategoryDTO category);
		Task<ServiceResponse<bool>> UpdateCategory(CategoryDTO categoryDTO);
		Task<ServiceResponse<bool>> DeleteCategory(int categoryId);
		Task<ServiceResponse<Category>> GetCategoryByUrl(string url);
		Task<int> GetCategoryCount();



	}
}
