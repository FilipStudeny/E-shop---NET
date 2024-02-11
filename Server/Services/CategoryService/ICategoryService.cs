using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared.DTOs.Series;

namespace Ecommerce.Server.Services.CategoryService
{
	public interface ICategoryService
	{
		Task<ServiceResponse<List<Category>>> GetCategories(bool getAll = false);
		Task<ServiceResponse<Category>> AddCategory(CategoryDTO category);
		Task<ServiceResponse<bool>> UpdateCategory(CategoryDTO categoryDTO);
		Task<ServiceResponse<bool>> DeleteCategory(int categoryId);
		Task<ServiceResponse<Category>> GetCategoryByUrl(string url);
		Task<int> GetCategoryCount();

		Task<ServiceResponse<bool>> CreateCategory(Category category);
		Task<ServiceResponse<bool>> UpdateCategory(Category category);

		Task<ServiceResponse<List<DataSelectDTO>>> GetCategoryNames();

	}
}
