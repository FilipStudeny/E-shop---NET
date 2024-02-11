using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared.DTOs.Series;
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

		public async Task<ServiceResponse<bool>> Add(Category category)
		{
			var response = await httpClient.PostAsJsonAsync("api/category/admin/add", category);
			var responseContent = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
			if (responseContent == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Error creating new category" };
			}
			return responseContent;
		}

		public async Task<ServiceResponse<bool>> Update(Category category)
		{
			var response = await httpClient.PutAsJsonAsync("api/category/admin/update", category);
			var responseData = (await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>());
			if (responseData == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Failed to update category, try again later" };
			}
			return responseData;
		}

		public async Task<ServiceResponse<Category>> GetCategory(string name)
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<Category>>($"api/category/admin/{name}");
			return response!;
		}
	}
}
