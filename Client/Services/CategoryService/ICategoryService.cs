using Ecommerce.Shared.Books;

namespace Ecommerce.Client.Services.CategoryService
{
	public interface ICategoryService
	{
		event Action? OnChange;
		List<Category> Categories { get; set; }
		string Message { get; set; }


		Task GetCategories();
	}
}
