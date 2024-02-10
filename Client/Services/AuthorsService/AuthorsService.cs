using Ecommerce.Client.Pages;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared.DTOs.Books;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace Ecommerce.Client.Services.AuthorsService
{
	public class AuthorsService : IAuthorsService
	{
		private readonly HttpClient httpClient;

		public List<Author> Authors { get; set; } = new();
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
		public string Message { get; set; } = string.Empty;

		public int AuthorsCount { get; set; }

		public event Action? OnChange;

        public AuthorsService(HttpClient httpClient)
        {
			this.httpClient = httpClient;
		}

        public async Task<bool> GetAuthors(int Page, bool getAll = false)
		{
			var url = getAll == false ? $"api/authors/{Page}" : $"api/authors/admin/{Page}";
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<Author>>>(url);

			if (response is { Data: not null })
			{
				Authors = response.Data;
				CurrentPage = response.CurrentPage;
				PageCount = response.NumberOfPages;
				AuthorsCount = response.ItemCount;
			}

			if (response?.Success == false)
			{
				Message = response.Message;
				return false;
			}

			OnChange?.Invoke();
			return true;
		}

		public async Task<ServiceResponse<List<DataSelectDTO>>> GetAllAuthorsNames()
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<DataSelectDTO>>>($"api/authors/admin/all");
			return response!;
		}

		public async Task<ServiceResponse<AuthorDTO>> GetAuthor(string Name)
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<AuthorDTO>>($"api/authors/author/{Name}");
			return response!;
		}

		public async Task<ServiceResponse<EditAuthorModel>> GetAuthorForEdit(string name)
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<EditAuthorModel>>($"api/authors/admin/author/{name}");
			return response!;
		}

		public async Task<ServiceResponse<bool>> UpdateAuthor(EditAuthorModel editAuthorModel, string urlName)
		{
			var response = await httpClient.PutAsJsonAsync($"api/authors/admin/author/update", editAuthorModel);
			var responseData = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

			if (responseData == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Failed to update author, try again later" };
			}

			return responseData;
		}

		public async Task<ServiceResponse<bool>> CreateAuthor(EditAuthorModel editAuthorModel)
		{
			var response = await httpClient.PostAsJsonAsync("api/authors/admin/author/add", editAuthorModel);
			var responseContent = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
			if (responseContent == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Error creating new author" };
			}
			return responseContent;
		}
	}
}
