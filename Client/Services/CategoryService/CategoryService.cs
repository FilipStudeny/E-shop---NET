using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs.Authors;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.CategoryService
{
	public class CategoryService : ICategoryService
	{
		public event Action? OnChange;

		public List<Category> Categories { get; set; } = new();
		public string Message { get; set; }

		private readonly HttpClient httpClient;

		public CategoryService(HttpClient httpClient)
        {
			this.httpClient = httpClient;
		}

		
		public async Task GetCategories(bool getAll)
		{
			var response = getAll == false ?
				await httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>($"api/category") :
				await httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>($"api/category/admin");

			if (response is { Data: not null })
			{
				Categories = response.Data;
			}

			if (Categories.Count == 0)
			{
				Message = "No series found.";
			}

			OnChange?.Invoke();
		}

		public async Task<ServiceResponse<List<DataSelectDTO>>> GetAllCategoryNames()
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<DataSelectDTO>>>($"api/category/admin/all");
			return response!;

		}
	}
}
