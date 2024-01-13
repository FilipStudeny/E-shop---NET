using Ecommerce.Client.Pages;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.AuthorsService
{
	public class AuthorsService : IAuthorsService
	{
		private readonly HttpClient httpClient;

		public List<Author> Authors { get; set; } = new();
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
		public string Message { get; set; } = string.Empty;

		public event Action? OnChange;

        public AuthorsService(HttpClient httpClient)
        {
			this.httpClient = httpClient;
		}

        public async Task<bool> GetAuthors(int Page)
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<Author>>>($"api/authors/{Page}");
			if (response is { Data: not null })
			{
				Authors = response.Data;
				CurrentPage = response.CurrentPage;
				PageCount = response.NumberOfPages;
			}

			if (response?.Success == false)
			{
				Message = response.Message;
				return false;
			}

			OnChange?.Invoke();
			return true;

		}

		public async Task<ServiceResponse<AuthorDTO>> GetAuthor(string Name)
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<AuthorDTO>>($"api/authors/author/{Name}");
			return response!;
		}
	}
}
