using Ecommerce.Client.Pages;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Authors;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;

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
	}
}
