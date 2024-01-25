using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs.Authors;

namespace Ecommerce.Client.Services.CategoryService
{
	public interface ICategoryService
	{
		event Action? OnChange;
		List<Category> Categories { get; set; }
		string Message { get; set; }


		Task GetCategories(bool getAll = false);
		Task<ServiceResponse<List<DataSelectDTO>>> GetAllCategoryNames();

	}
}
